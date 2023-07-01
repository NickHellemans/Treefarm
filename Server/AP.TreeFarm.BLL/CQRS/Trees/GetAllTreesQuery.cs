using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Trees;
public class GetAllTreesQuery : IRequest<IEnumerable<TreeDTO>>
{

}

public class GetAllTreesQueryHandler : IRequestHandler<GetAllTreesQuery, IEnumerable<TreeDTO>>
{
    private readonly IUnitofWork _uow;
    private readonly IMapper _mapper;

    public GetAllTreesQueryHandler(IUnitofWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TreeDTO>> Handle(GetAllTreesQuery request, CancellationToken cancellationToken)
    {
        var list = await _uow.TreeRepository.GetAll();
        return _mapper.Map<IEnumerable<TreeDTO>>(list);
    }
}