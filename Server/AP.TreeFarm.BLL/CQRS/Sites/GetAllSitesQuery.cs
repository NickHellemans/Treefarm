using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Sites;
public class GetAllSitesQuery : IRequest<IEnumerable<SiteDTO>>
{

}

public class GetAllSitesQueryHandler : IRequestHandler<GetAllSitesQuery, IEnumerable<SiteDTO>>
{
    private readonly IUnitofWork _uow;
    private readonly IMapper _mapper;

    public GetAllSitesQueryHandler(IUnitofWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SiteDTO>> Handle(GetAllSitesQuery request, CancellationToken cancellationToken)
    {
        var list = await _uow.SitesRepository.GetAll();
        return _mapper.Map<IEnumerable<SiteDTO>>(list);
    }
}