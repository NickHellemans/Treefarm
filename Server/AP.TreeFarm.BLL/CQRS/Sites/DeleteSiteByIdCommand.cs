using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Sites
{
    public class DeleteSiteByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteSiteByIdCommandHandler : IRequestHandler<DeleteSiteByIdCommand, int>
    {
        private readonly IUnitofWork uow;

        public DeleteSiteByIdCommandHandler(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(DeleteSiteByIdCommand request, CancellationToken cancellationToken)
        {
            var site = await uow.SitesRepository.GetById(request.Id);
            uow.SitesRepository.Delete(site);
            await uow.Commit();
            return request.Id;
        }
    }
}