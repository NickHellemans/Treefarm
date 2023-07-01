using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class PauseTreeTaskCommand : IRequest<UpdateTreeTaskDTO>
    {
        public int Id;
    }

    public class PauseTreeTaskCommandHandler : IRequestHandler<PauseTreeTaskCommand, UpdateTreeTaskDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;
        public PauseTreeTaskCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this._mapper = mapper;
        }

        public async Task<UpdateTreeTaskDTO> Handle(PauseTreeTaskCommand request, CancellationToken cancellationToken)
        {
            //Check if task
            var treeTask = await uow.TreeTasksRepository.GetById(request.Id);
            if (treeTask == null)
                throw new KeyNotFoundException("The task was not found");

            if (treeTask.Status == Domain.TaskStatus.InProgress)
            {
                treeTask.DatePaused = DateTime.Now;
                treeTask.Status = Domain.TaskStatus.Paused;
                uow.TreeTasksRepository.Update(treeTask);
                await uow.Commit();
            }
            return _mapper.Map<UpdateTreeTaskDTO>(treeTask);
        }
    }
}