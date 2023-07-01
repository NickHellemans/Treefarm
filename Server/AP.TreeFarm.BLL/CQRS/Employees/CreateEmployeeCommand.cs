using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RestSharp;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class CreateEmployeeCommand : IRequest<Tuple<CreateEmployeeDTO, List<ValidationFailure>>>
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand,
        Tuple<CreateEmployeeDTO, List<ValidationFailure>>>
    {
        private IValidator<CreateEmployeeDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper mapper;
        private readonly RestClient _restClient;


        public CreateEmployeeCommandHandler(IUnitofWork uow, IValidator<CreateEmployeeDTO> validator, IMapper mapper, RestClient restClient)
        {
            this.mapper = mapper;
            this.uow = uow;
            _validator = validator;
            _restClient = restClient;
        }

        public async Task<Tuple<CreateEmployeeDTO, List<ValidationFailure>>> Handle(CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                EmployeeId = request.EmployeeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.FirstName + request.LastName + request.EmployeeId,
                //Password = request.Password,
                IsActive = request.IsActive,
                IsAdmin = request.IsAdmin,
                Auth0Id = "PlaceHolderId"
            };

            var employeeCreateDto = new CreateEmployeeDTO()
            {
                EmployeeId = request.EmployeeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.FirstName + request.LastName + request.EmployeeId,
                Password = request.Password,
                IsActive = request.IsActive,
                IsAdmin = request.IsAdmin,
            };
            
            //ValidationResult result = await _validator.ValidateAsync(mapper.Map<CreateEmployeeDTO>(employee));
            ValidationResult result = await _validator.ValidateAsync(employeeCreateDto);
            if (!result.IsValid)
            {
                return Tuple.Create(mapper.Map<CreateEmployeeDTO>(employee), result.Errors);
            }

            const string domain = "dev-mlppzg45imwcpi8a.us.auth0.com";
            const string clientId = "uwpU75dQyIl2WGoa3hEbMtp1UaEWxi4N";
            const string clientSecret = "lz7roCCRVztHB2zfKOuiiM9c_nGYq9UFeYesgJhccC3Ku8EzGsVlKwjWmLgsTIox";
            
            //var client = new RestClient("https://dev-mlppzg45imwcpi8a.us.auth0.com/");
            
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
                result.Errors.Add(new ValidationFailure("Auth0:GetToken", "Er liep iets fout bij verkrijgen van de access-token op Auth0"));
                return Tuple.Create(mapper.Map<CreateEmployeeDTO>(employee), result.Errors);
            }
            
            //Request user create on auth0
            var requestCreateUser = new RestRequest("api/v2/users").AddJsonBody(new
            {
                name = request.FirstName + " " + request.LastName,
                email = request.Email,
                password = request.Password,
                //password = "kristofisdaddy!1",
                connection = "Username-Password-Authentication",
                verify_email = true
            });
            requestCreateUser.AddHeader("authorization", $"Bearer {mngToken}");
            var responseCreateUser = await _restClient.ExecutePostAsync<Auth0User>(requestCreateUser, cancellationToken: cancellationToken);
            if (responseCreateUser.Data != null)
            {
                var userId = responseCreateUser.Data.user_id;

                if (!responseCreateUser.IsSuccessful)
                {
                    var check = false;
                    if (responseCreateUser.Content != null &&
                        responseCreateUser.Content.Contains("The user already exists"))
                    {
                        check = true;
                        result.Errors.Add(new ValidationFailure("Auth0:ConflictUser", "Email bestaat al"));
                    }

                    if (responseCreateUser.Content != null &&
                        responseCreateUser.Content.Contains("PasswordStrengthError"))
                    {
                        check = true;
                        result.Errors.Add(new ValidationFailure("Auth0:WeakPass", "Wachtwoord is te zwak"));
                        if (request.Password.Length < 12)
                            result.Errors.Add(new ValidationFailure("Auth0:WeakPassLength",
                                "Wachtwoord moet minstens 12 karakters bevatten"));
                        if (!request.Password.Any(char.IsLower))
                            result.Errors.Add(new ValidationFailure("Auth0:WeakPassLowerCase",
                                "Wachtwoord moet minstens 1 kleine letter bevatten (a-z)"));
                        if (!request.Password.Any((char.IsUpper)))
                            result.Errors.Add(new ValidationFailure("Auth0:WeakPassUpperCase",
                                "Wachtwoord moet minstens 1 hoofdletter bevatten (A-Z)"));
                        if (!request.Password.Any(char.IsDigit))
                            result.Errors.Add(new ValidationFailure("Auth0:WeakPassNumber",
                                "Wachtwoord moet minstens 1 nummer bevatten (0-9)"));
                    }

                    if (!check)
                    {
                        result.Errors.Add(new ValidationFailure("Auth0:Create:User", "Er liep iets fout bij het aanmaken van de gebruiker op Auth0"));
                    }
                    return Tuple.Create(mapper.Map<CreateEmployeeDTO>(employee), result.Errors);
                }
            
                const string adminRole = "rol_Uwc2D00ZQq2vJ0LK";
                const string employeeRole = "rol_XbAaQzCIiu4ZSfAh";
                //Assign roles to user on auth0
                var userRoles = new List<string>
                {
                    employeeRole
                };

                if (request.IsAdmin)
                    userRoles.Add(adminRole);

                var requestAssignRoles = new RestRequest("api/v2/users/" + userId + "/roles")
                    .AddJsonBody(new
                    {
                        roles = userRoles
                    });
                requestAssignRoles.AddHeader("authorization", $"Bearer {mngToken}");
                var responseAssignRoles =
                    await _restClient.ExecutePostAsync(requestAssignRoles, cancellationToken: cancellationToken);

                if (!responseAssignRoles.IsSuccessful)
                {
                    result.Errors.Add(new ValidationFailure("Auth0:AssignRoles", "Er liep iets fout bij het aanmaken van de rollen voor de gebruiker op Auth0")); 
                    return Tuple.Create(mapper.Map<CreateEmployeeDTO>(employee), result.Errors);
                }

                employee.Auth0Id = userId;
            }

            uow.EmployeesRepository.Create(employee);
            await uow.Commit();

            return Tuple.Create(mapper.Map<CreateEmployeeDTO>(employee), result.Errors);
        }
    }

    public class TokenObject
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }

    public class Auth0User
    {
        public string email { get; set; }
        public string user_id { get; set; }
    }
}