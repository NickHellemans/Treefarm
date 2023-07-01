using System;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.CQRS.Trees;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP.MyTreeFarm.WebAPI.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class TreeController : APIv1Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public TreeController(IMediator mediator, IValidator<CreateTreeDTO> validator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]  
        public async Task<IActionResult> GetAllTrees()
        {
            return Ok(await this.mediator.Send(new GetAllTreesQuery())); 
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTree(int id)
        {
            var tree = await mediator.Send(new GetTreeByIdQuery() { Id = id });
            if (tree == null)
                return NotFound(); 

            return Ok(tree);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTree([FromBody]CreateTreeCommand tree)
        {

            var result = await mediator.Send(tree);

            if (result.Item2.Count > 0)
            {
                return BadRequest(result);

            }

            return Created("", result.Item1);

            
        }
        
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTree(int id, [FromBody]UpdateTreeCommand updatedTree)
        {
            updatedTree.Id = id;
            
            var result = await mediator.Send(updatedTree);
            var result2 = Tuple.Create(mapper.Map<TreeDTO>(result.Item1), result.Item2);
            if (result.Item2.Count > 0)
            {
                return BadRequest(result2);

            }
            
            return Ok(await mediator.Send(updatedTree));
        }
        [Route("{id}")]  //api/people/id
        [HttpDelete]
        public async Task<IActionResult> DeleteTree (int id, [FromHeader(Name ="X-AccessKey")]string AccessKey)
        {
            //if(AccessKey != "123456789") return Unauthorized();
            return Ok(await mediator.Send(new DeleteTreeByIdCommand { Id = id }));
        }
        
    }
}