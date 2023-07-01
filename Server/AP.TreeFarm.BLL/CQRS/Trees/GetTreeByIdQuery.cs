using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Trees
{
    public class GetTreeByIdQuery : IRequest<TreeDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetTreeByIdQuery, TreeDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<TreeDTO> Handle(GetTreeByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TreeDTO>(await uow.TreeRepository.GetById(request.Id));
        }
    }
}