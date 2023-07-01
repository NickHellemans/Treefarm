using System;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP.MyTreeFarm.WebAPI.Controllers
{
    public class TreeTaskController : APIv1Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public TreeTaskController(IMediator mediator, IValidator<CreateTreeTaskDTO> validator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet] 
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> GetAllTasks()
        {
            var test = await this.mediator.Send(new GetAllTreeTasksQuery());
            return Ok(test); 
        }
        
        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = "EmployeeAccess")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await mediator.Send(new GetTreeTaskByIdQuery() { Id = id });
            if (task == null)
                return NotFound(); 

            return Ok(task);
        }
        
        [HttpPost]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> CreateTreeTask([FromBody]CreateTreeTaskCommand task)
        {
            var result = await mediator.Send(task);
            
            if (result.Item2.Count > 0)
            {
                return BadRequest(result);
            }
            return Created("", result.Item1);
            
        }
        
        [HttpPost]
        [Authorize(Policy = "AdminAccess")]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody]UpdateTreeTaskCommand updatedTreeTask)
        {
            updatedTreeTask.Id = id;


            var result = await mediator.Send(updatedTreeTask);
            var result2 = Tuple.Create(mapper.Map<UpdateTreeTaskDTO>(result.Item1), result.Item2);
            if (result.Item2.Count > 0)
            {
                return BadRequest(result2);

            }

            return Ok(await mediator.Send(updatedTreeTask));
        }
        
        [HttpGet]
        [Authorize(Policy = "EmployeeAccess")]
        [Route("StartTask/{id}")]
        public async Task<IActionResult> StartTask(int id)
        {
            return Ok(await mediator.Send(new StartTreeTaskCommand() { Id = id }));
        }
        
        [HttpGet]
        [Authorize(Policy = "EmployeeAccess")]
        [Route("PauseTask/{id}")]
        public async Task<IActionResult> PauseTask(int id)
        {
            return Ok(await mediator.Send(new PauseTreeTaskCommand() { Id = id }));
        }
        
        [HttpGet]
        [Authorize(Policy = "EmployeeAccess")]
        [Route("StopTask/{id}")]
        public async Task<IActionResult> StopTask(int id)
        {
            return Ok(await mediator.Send(new StopTreeTaskCommand() { Id = id }));
        }
        
        [Route("{id}")]  //api/people/id
        [HttpDelete]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> DeleteTreeTask (int id, [FromHeader(Name ="X-AccessKey")]string AccessKey)
        {
            //if(AccessKey != "123456789") return Unauthorized();
            return Ok(await mediator.Send(new DeleteTreeTaskByIdCommand { Id = id }));
        }
    }
}
