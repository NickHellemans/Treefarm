using System.Threading.Tasks;
using System;
using AP.MyTreeFarm.Application.CQRS.Employees;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP.MyTreeFarm.WebAPI.Controllers
{
    public class EmployeeController : APIv1Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        

        public EmployeeController(IMediator mediator, IValidator<CreateEmployeeDTO> validator, IMapper mapper)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await this.mediator.Send(new GetAllEmployeesQuery());
            if (employees == null)
                return NotFound();
            return Ok(employees); 
        }
        
        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = "EmployeeAccess")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await mediator.Send(new GetEmployeeByIdQuery() { Id = id });
            if (employee == null)
                return NotFound(); 

            return Ok(employee);
        }
        
        [HttpGet]
        [Route("email/{email}")]
        [Authorize(Policy = "EmployeeAccess")]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
            var employee = await mediator.Send(new GetEmployeeByEmailQuery() { Email = email });
            if (employee == null)
                return NotFound(); 

            return Ok(employee);
        }
        
        [HttpPost]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeCommand employee)
        {
            var result = await mediator.Send(employee);
            
            if (result.Item2.Count  > 0)
            {
                return BadRequest(result);
                
            }

            return Created("", result.Item1);

        }
        
        [HttpPost]
        [Route("{id}")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody]UpdateEmployeeCommand updatedEmployee)
        {
            updatedEmployee.Id = id;

            var result = await mediator.Send(updatedEmployee);
            var result2 = Tuple.Create(mapper.Map<EmployeeDTO>(result.Item1), result.Item2);
            if (result.Item2.Count > 0)
            {
                return BadRequest(result2);

            }

            return Ok(await mediator.Send(updatedEmployee));
            //return Ok(result2);
        }
        
        [Route("{id}")]  //api/people/id
        [HttpDelete]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> DeleteEmployee (int id, [FromHeader(Name ="X-AccessKey")]string accessKey)
        {
            return Ok(await mediator.Send(new DeleteEmployeeByIdCommand { Id = id }));
        }
        
    }
}