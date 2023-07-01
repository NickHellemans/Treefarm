using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Employees;
public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDTO>>
{

}

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IUnitofWork _uow;
    private readonly IMapper _mapper;

    public GetAllEmployeesQueryHandler(IUnitofWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<EmployeeDTO>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var list = await _uow.EmployeesRepository.GetAll();
        return _mapper.Map<IEnumerable<EmployeeDTO>>(list);
    }
}