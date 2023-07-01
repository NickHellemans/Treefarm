using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Exceptions;
using AP.MyTreeFarm.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class UpdateTreeTaskCommand : IRequest<Tuple<UpdateTreeTaskDTO, List<ValidationFailure>>>
    {
        public int Id;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int EmployeeId { get; set; }
        public int ZoneId { get; set; }
        public TaskStatus Status { get; set; }
        
        public int Priority { get; set;  }
    
        public DateTime DatePlanned { get; set; }
    }

    public class UpdateTreeTaskCommandHandler : IRequestHandler<UpdateTreeTaskCommand, Tuple<UpdateTreeTaskDTO, List<ValidationFailure>>>
    {
        private IValidator<UpdateTreeTaskDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;
        public UpdateTreeTaskCommandHandler(IUnitofWork uow, IMapper mapper, IValidator<UpdateTreeTaskDTO> validator)
        {
            this._validator = validator;
            this.uow = uow;
            this._mapper = mapper;
        }

        public async Task<Tuple<UpdateTreeTaskDTO, List<ValidationFailure>>> Handle(UpdateTreeTaskCommand request, CancellationToken cancellationToken)
        {
            //Check if task, employee and zone exist
            var treeTask = await uow.TreeTasksRepository.GetById(request.Id);
            if (treeTask == null)
                throw new KeyNotFoundException("The task was not found");
            
            var employee = await uow.EmployeesRepository.GetById(request.EmployeeId);
            if (employee == null) 
                throw new RelationNotFoundException("The specified employee does not exist");
                
            var zone = await uow.ZonesRepository.GetById(request.ZoneId);
            if (zone == null)
                throw new RelationNotFoundException("The specified zone does not exist");   
            
            //Update values
            treeTask.Name = request.Name;
            treeTask.Description = request.Description;
            treeTask.Duration = request.Duration;
            treeTask.EmployeeId = request.EmployeeId;
            treeTask.ZoneId = request.ZoneId;
            treeTask.Status = (Domain.TaskStatus)request.Status;
            treeTask.Priority = request.Priority;
            treeTask.DatePlanned = request.DatePlanned;

            var result = await _validator.ValidateAsync(_mapper.Map<UpdateTreeTaskDTO>(treeTask), cancellationToken);

            if (!result.IsValid)
            {
                // return Tuple.Create(treeTaskDTO, result.Errors);
                return Tuple.Create(_mapper.Map<UpdateTreeTaskDTO>(treeTask), result.Errors);

            }

            uow.TreeTasksRepository.Update(treeTask);
            await uow.Commit();
            return Tuple.Create(_mapper.Map<UpdateTreeTaskDTO>(treeTask), result.Errors);
        }
    }
}