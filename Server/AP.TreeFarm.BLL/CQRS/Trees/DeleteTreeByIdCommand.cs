using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Trees
{
    public class DeleteTreeByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTreeByIdCommandHandler : IRequestHandler<DeleteTreeByIdCommand, int>
    {
        private readonly IUnitofWork uow;

        public DeleteTreeByIdCommandHandler(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(DeleteTreeByIdCommand request, CancellationToken cancellationToken)
        {
            var tree = await uow.TreeRepository.GetById(request.Id);
            uow.TreeRepository.Delete(tree);
            await uow.Commit();
            return request.Id;
        }
    }
}