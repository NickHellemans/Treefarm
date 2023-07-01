using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Employees
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmployeeDTO>(await uow.EmployeesRepository.GetById(request.Id));
        }
    }
}