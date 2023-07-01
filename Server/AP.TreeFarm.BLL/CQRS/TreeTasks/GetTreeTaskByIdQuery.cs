using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class GetTreeTaskByIdQuery : IRequest<TreeTaskDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetTreeTaskByIdQuery, TreeTaskDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<TreeTaskDTO> Handle(GetTreeTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TreeTaskDTO>(await uow.TreeTasksRepository.GetById(request.Id));
        }
    }
}