using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class StopTreeTaskCommand : IRequest<UpdateTreeTaskDTO>
    {
        public int Id;
    }

    public class StopTreeTaskCommandHandler : IRequestHandler<StopTreeTaskCommand, UpdateTreeTaskDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;
        public StopTreeTaskCommandHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            this._mapper = mapper;
        }

        public async Task<UpdateTreeTaskDTO> Handle(StopTreeTaskCommand request, CancellationToken cancellationToken)
        {
            //Check if task, employee and zone exist
            var treeTask = await uow.TreeTasksRepository.GetById(request.Id);
            if (treeTask == null)
                throw new KeyNotFoundException("The task was not found");

            if (treeTask.Status is Domain.TaskStatus.InProgress or Domain.TaskStatus.Paused)
            { 
                //Set end date + change status
                treeTask.DateEnd = DateTime.Now;
                treeTask.Status = Domain.TaskStatus.Done;
                uow.TreeTasksRepository.Update(treeTask);
                await uow.Commit();
            }
            
            return _mapper.Map<UpdateTreeTaskDTO>(treeTask);
        }
    }
}