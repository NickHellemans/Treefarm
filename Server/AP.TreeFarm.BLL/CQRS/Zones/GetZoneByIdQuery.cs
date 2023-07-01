using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Zones
{
    public class GetZoneByIdQuery : IRequest<ZoneDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetZoneByIdQuery, ZoneDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<ZoneDTO> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ZoneDTO>(await uow.ZonesRepository.GetById(request.Id));
        }
    }
}