using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using MediatR;
using RestSharp;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class DeleteEmployeeByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, int>
    {
        private readonly IUnitofWork uow;
        private readonly RestClient _restClient;
        
        public DeleteEmployeeByIdCommandHandler(IUnitofWork uow, RestClient restClient)
        {
            this.uow = uow;
            _restClient = restClient;
        }

        public async Task<int> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await uow.EmployeesRepository.GetById(request.Id);
            if (employee == null)
                throw new KeyNotFoundException("The employee was not found");
            
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
            //Request delete user on auth0
            var requestDeleteUser = new RestRequest("api/v2/users/" + employee.Auth0Id, Method.Delete);
            requestDeleteUser.AddHeader("authorization", $"Bearer {mngToken}");
            var responseDeleteUser = await _restClient.ExecuteAsync(requestDeleteUser, cancellationToken: cancellationToken);
            
            if (!responseDeleteUser.IsSuccessful)
            {
                throw new KeyNotFoundException("Gebruik niet gevonden op Auth0");
            }
            
            uow.EmployeesRepository.Delete(employee);
            await uow.Commit();
            return request.Id;
        }
    }
}