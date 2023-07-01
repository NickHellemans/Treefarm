using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Sites
{
    public class GetSiteByIdQuery : IRequest<SiteDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetSiteByIdQuery, SiteDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<SiteDTO> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<SiteDTO>(await uow.SitesRepository.GetById(request.Id));
        }
    }
}