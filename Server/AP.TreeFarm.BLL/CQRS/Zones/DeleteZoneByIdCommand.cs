using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.Zones
{
    public class DeleteZoneByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteZoneByIdCommandHandler : IRequestHandler<DeleteZoneByIdCommand, int>
    {
        private readonly IUnitofWork uow;

        public DeleteZoneByIdCommandHandler(IUnitofWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(DeleteZoneByIdCommand request, CancellationToken cancellationToken)
        {
            var zone = await uow.ZonesRepository.GetById(request.Id);
            uow.ZonesRepository.Delete(zone);
            await uow.Commit();
            return request.Id;
        }
    }
}