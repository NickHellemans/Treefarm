using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class StartTreeTaskCommand : IRequest<UpdateTreeTaskDTO>
    {
        public int Id;
    }

    public class StartTreeTaskCommandHandler : IRequestHandler<StartTreeTaskCommand, UpdateTreeTaskDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;
        public StartTreeTaskCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this._mapper = mapper;
        }

        public async Task<UpdateTreeTaskDTO> Handle(StartTreeTaskCommand request, CancellationToken cancellationToken)
        {
            //Check if task, employee and zone exist
            var treeTask = await uow.TreeTasksRepository.GetById(request.Id);
            if (treeTask == null)
                throw new KeyNotFoundException("The task was not found");

            if (treeTask.Status == Domain.TaskStatus.ToDo)
            {
                //Set start date
                treeTask.DateStart = DateTime.Now;
                treeTask.Status = Domain.TaskStatus.InProgress;
            }
            
            if (treeTask.Status == Domain.TaskStatus.Paused)
            {
                if (treeTask.DatePaused.HasValue)
                {
                    TimeSpan pauseTimer = DateTime.Now.Subtract(treeTask.DatePaused.Value);
                    treeTask.TimePaused += pauseTimer.TotalSeconds;
                    treeTask.Status = Domain.TaskStatus.InProgress;   
                }
            }
            
            uow.TreeTasksRepository.Update(treeTask);
            await uow.Commit();
            return _mapper.Map<UpdateTreeTaskDTO>(treeTask);
        }
    }
}