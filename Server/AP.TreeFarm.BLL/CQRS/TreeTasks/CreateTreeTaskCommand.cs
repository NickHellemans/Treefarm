using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.Exceptions;
using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Domain;
using AutoMapper;
using MediatR;
using FluentValidation;
using FluentValidation.Results;
using TaskStatus = AP.MyTreeFarm.Domain.TaskStatus;

namespace AP.MyTreeFarm.Application.CQRS.TreeTasks
{
    public class CreateTreeTaskCommand : IRequest<Tuple<CreateTreeTaskDTO,List<ValidationFailure>>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int EmployeeId { get; set; }
        public int ZoneId { get; set; }
        
        public int Priority { get; set; }
        
        public DateTime DatePlanned { get; set; }
        
    }

    public class CreateTreeTaskCommandHandler : IRequestHandler<CreateTreeTaskCommand, Tuple<CreateTreeTaskDTO,List<ValidationFailure>>>
    {
        private IValidator<CreateTreeTaskDTO> _validator;
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public CreateTreeTaskCommandHandler(IUnitofWork uow, IMapper mapper,IValidator<CreateTreeTaskDTO> validator)
        {
            this._validator = validator;
            this.uow = uow;
            this._mapper = mapper;
        }

        public async Task<Tuple<CreateTreeTaskDTO,List<ValidationFailure>>> Handle(CreateTreeTaskCommand request, CancellationToken cancellationToken)
        {
            
            var employee = await uow.EmployeesRepository.GetById(request.EmployeeId);
            if (employee == null) 
                    throw new RelationNotFoundException("The specified employee does not exist");
                
            var zone = await uow.ZonesRepository.GetById(request.ZoneId);
            if (zone == null)
                throw new RelationNotFoundException("The specified zone does not exist");
            
            var task = new TreeTask()
            {
                Name = request.Name,
                Description = request.Description,
                DateCreated = DateTime.Now,
                DateStart = null,
                DateEnd = null,
                Duration = request.Duration,
                EmployeeId = request.EmployeeId,
                ZoneId = request.ZoneId,
                Status = TaskStatus.ToDo,
                Employee = employee,
                Zone = zone,
                Priority = request.Priority,
                DatePlanned = request.DatePlanned,
                DatePaused = null,
                TimePaused = 0
            };
            var treeTaskDTO = _mapper.Map<CreateTreeTaskDTO>(task);
            var result = await _validator.ValidateAsync(treeTaskDTO, cancellationToken);
            if (!result.IsValid)
            {
               // return Tuple.Create(treeTaskDTO, result.Errors);
                return Tuple.Create(_mapper.Map<CreateTreeTaskDTO>(task), result.Errors);

            }
            
            uow.TreeTasksRepository.Create(task);
            await uow.Commit();
            //return task;
            //return Tuple.Create(treeTaskDTO, result.Errors);
            return Tuple.Create(_mapper.Map<CreateTreeTaskDTO>(task), result.Errors);
        }
    }
}