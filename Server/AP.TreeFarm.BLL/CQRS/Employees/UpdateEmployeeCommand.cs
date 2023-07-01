using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RestSharp;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class UpdateEmployeeCommand : IRequest<Tuple<UpdateEmployeeDTO, List<ValidationFailure>>>
    {
        public int Id;
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Tuple<UpdateEmployeeDTO, List<ValidationFailure>>>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        private IValidator<UpdateEmployeeDTO> _validator;
        private readonly RestClient _restClient;


        public UpdateEmployeeCommandHandler(IUnitofWork uow, IMapper mapper, IValidator<UpdateEmployeeDTO> validator, RestClient restClient)
        {
            this.uow = uow;
            this.mapper = mapper;
            _validator = validator;
            _restClient = restClient;
        }

        public async Task<Tuple<UpdateEmployeeDTO, List<ValidationFailure>>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await uow.EmployeesRepository.GetById(request.Id);
            if (employee == null)
                throw new KeyNotFoundException("The employee was not found");

            //Store temp employee data to check before making otherwise call to Auth0 API
            var tempEmail = employee.Email; 
            var tempFirstName = employee.FirstName;
            var tempLastName = employee.LastName;
            var tempIsAdmin = employee.IsAdmin;
            
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.EmployeeId = request.EmployeeId;
            employee.Email = request.Email;
            employee.UserName = request.FirstName + request.LastName + request.EmployeeId;
            employee.IsActive = request.IsActive;
            employee.IsAdmin = request.IsAdmin;

            ValidationResult result = await _validator.ValidateAsync(mapper.Map<UpdateEmployeeDTO>(employee));
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<UpdateEmployeeDTO>(employee), result.Errors);
            }
            
            const string domain = "dev-mlppzg45imwcpi8a.us.auth0.com";
            const string clientId = "uwpU75dQyIl2WGoa3hEbMtp1UaEWxi4N";
            const string clientSecret = "lz7roCCRVztHB2zfKOuiiM9c_nGYq9UFeYesgJhccC3Ku8EzGsVlKwjWmLgsTIox";
            
            //Request token for management API
            var requestMngApiToken = new RestRequest("oauth/token");
            requestMngApiToken.AddHeader("content-type", "application/json");
            requestMngApiToken
                .AddParameter("application/json", $"{{\"client_id\":\"{clientId}\",\"client_secret\":\"{clientSecret}\",\"audience\":\"https://{domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}",
                    ParameterType.RequestBody);
            var response =
                await _restClient.ExecutePostAsync<TokenObject>(requestMngApiToken, cancellationToken: cancellationToken);
            var mngToken = response.Data.access_token;

            if (!response.IsSuccessful)
            {
                throw new KeyNotFoundException("Er liep iets fout met het verkrijgen van de token");
            }
            
            //Request update user on auth0
            if (tempEmail != request.Email || tempFirstName != request.FirstName || tempLastName != request.LastName)
            {
                var requestUpdateUser = new RestRequest("api/v2/users/" + employee.Auth0Id, Method.Patch).AddJsonBody(new
                {
                    name = request.FirstName + " " + request.LastName,
                    email = request.Email
                });
                requestUpdateUser.AddHeader("authorization", $"Bearer {mngToken}");
                var responseUpdateUser = await _restClient.ExecuteAsync<Auth0User>(requestUpdateUser, cancellationToken: cancellationToken);

                if (!responseUpdateUser.IsSuccessful)
                {
                    if (responseUpdateUser.Content != null && responseUpdateUser.Content.Contains("The specified new email already exists"))
                        result.Errors.Add(new ValidationFailure("Auth0:ConflictUser", "Email bestaat al"));
                    else
                    {
                        result.Errors.Add(new ValidationFailure("Auth0:Update:User", "Er liep iets fout bij het updaten van de gebruiker op Auth0"));
                    }
                    
                    return Tuple.Create(mapper.Map<UpdateEmployeeDTO>(employee), result.Errors);
                }
            }
            
            //Update roles for user on auth0
            const string adminRole = "rol_Uwc2D00ZQq2vJ0LK";
            const string employeeRole = "rol_XbAaQzCIiu4ZSfAh";

            switch (tempIsAdmin)
            {
                //Delete admin role if no longer admin
                case true when request.IsAdmin == false:
                {
                    var rolesToDelete = new List<string>
                    {
                        adminRole
                    };
                    var requestDeleteRoles = new RestRequest("api/v2/users/" + employee.Auth0Id + "/roles", Method.Delete)
                        .AddJsonBody(new { roles = rolesToDelete });
                    requestDeleteRoles.AddHeader("authorization", $"Bearer {mngToken}");
                    var responseDeleteRoles = await _restClient.ExecuteAsync(requestDeleteRoles, cancellationToken: cancellationToken);
                
                    if (!responseDeleteRoles.IsSuccessful)
                    {
                        result.Errors.Add(new ValidationFailure("Auth0:DeleteRoles", "Er liep iets fout bij het verwijderen van de rollen voor de gebruiker op Auth0")); 
                        return Tuple.Create(mapper.Map<UpdateEmployeeDTO>(employee), result.Errors);
                    }

                    break;
                }
                //Add admin role if not yet admin already
                case false when request.IsAdmin:
                {
                    var rolesToAssign = new List<string>
                    {
                        adminRole
                    };
                    
                    var requestAssignRoles = new RestRequest("api/v2/users/" + employee.Auth0Id + "/roles")
                        .AddJsonBody(new { roles = rolesToAssign });
                    requestAssignRoles.AddHeader("authorization", $"Bearer {mngToken}");
                    var responseAssignRoles = await _restClient.ExecutePostAsync(requestAssignRoles, cancellationToken: cancellationToken);

                    if (!responseAssignRoles.IsSuccessful)
                    {
                        result.Errors.Add(new ValidationFailure("Auth0:AssignRoles", "Er liep iets fout bij het aanpassen van de rollen voor de gebruiker op Auth0")); 
                        return Tuple.Create(mapper.Map<UpdateEmployeeDTO>(employee), result.Errors);
                    }
                    break;
                }
            }
            
            uow.EmployeesRepository.Update(employee);
            await uow.Commit();
            return Tuple.Create(mapper.Map<UpdateEmployeeDTO>(employee), result.Errors);
        }
    }
}