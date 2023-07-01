using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Zones;
public class GetAllZonesQuery : IRequest<IEnumerable<ZoneDTO>>
{

}

public class GetAllZonesQueryHandler : IRequestHandler<GetAllZonesQuery, IEnumerable<ZoneDTO>>
{
    private readonly IUnitofWork _uow;
    private readonly IMapper _mapper;

    public GetAllZonesQueryHandler(IUnitofWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ZoneDTO>> Handle(GetAllZonesQuery request, CancellationToken cancellationToken)
    {
        var list = await _uow.ZonesRepository.GetAll();
        return _mapper.Map<IEnumerable<ZoneDTO>>(list);
    }
}