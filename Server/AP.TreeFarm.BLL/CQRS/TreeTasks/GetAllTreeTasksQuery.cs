using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks;
public class GetAllTreeTasksQuery : IRequest<IEnumerable<TreeTaskDTO>>
{

}

public class GetAllTreeTasksQueryHandler : IRequestHandler<GetAllTreeTasksQuery, IEnumerable<TreeTaskDTO>>
{
    private readonly IUnitofWork _uow;
    private readonly IMapper _mapper;

    public GetAllTreeTasksQueryHandler(IUnitofWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TreeTaskDTO>> Handle(GetAllTreeTasksQuery request, CancellationToken cancellationToken)
    {
        var list = await _uow.TreeTasksRepository.GetAll();
        return _mapper.Map<IEnumerable<TreeTaskDTO>>(list);
    }
}