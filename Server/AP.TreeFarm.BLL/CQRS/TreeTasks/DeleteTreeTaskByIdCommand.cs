using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class DeleteTreeTaskByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTreeTaskByIdCommandHandler : IRequestHandler<DeleteTreeTaskByIdCommand, int>
    {
        private readonly IUnitofWork uow;

        public DeleteTreeTaskByIdCommandHandler(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(DeleteTreeTaskByIdCommand request, CancellationToken cancellationToken)
        {
            var task = await uow.TreeTasksRepository.GetById(request.Id);
            uow.TreeTasksRepository.Delete(task);
            await uow.Commit();
            return request.Id;
        }
    }
}