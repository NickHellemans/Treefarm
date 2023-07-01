using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class GetEmployeeByEmailQuery : IRequest<EmployeeDTO>
    {
        public string Email { get; set; }
    }

    public class GetByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, EmployeeDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetByEmailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<EmployeeDTO> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmployeeDTO>(await uow.EmployeesRepository.GetByEmail(request.Email));
        }
    }
}