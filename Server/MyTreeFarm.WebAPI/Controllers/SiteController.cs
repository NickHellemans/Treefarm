using System;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.CQRS.Sites;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP.MyTreeFarm.WebAPI.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class SiteController : APIv1Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;


        public SiteController(IMediator mediator, IValidator<CreateSiteDTO> validator, IMapper mapper)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]   //api/people?lastname=Janssens
        public async Task<IActionResult> GetAllSites()
        {
            var sites = await this.mediator.Send(new GetAllSitesQuery());
            if (sites == null) 
                return NotFound();
            return Ok(sites); 
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSite(int id)
        {
            var site = await mediator.Send(new GetSiteByIdQuery() { Id = id });
            if (site == null)
                return NotFound();
            return Ok(site);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSite([FromBody]CreateSiteCommand site)
        {
            var result = await mediator.Send(site);

            if (result.Item2.Count > 0)
            {
                return BadRequest(result);

            }

            return Created("", result.Item1);
        }
        
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSite(int id, [FromBody]UpdateSiteCommand updatedSite)
        {
            updatedSite.Id = id;
            var result = await mediator.Send(updatedSite);
            var result2 = Tuple.Create(mapper.Map<SiteDTO>(result.Item1), result.Item2);
            if (result.Item2.Count > 0)
            {
                return BadRequest(result2);

            }
            return Ok(await mediator.Send(updatedSite));
        }
        [Route("{id}")]  //api/people/id
        [HttpDelete]
        public async Task<IActionResult> DeleteSite (int id, [FromHeader(Name ="X-AccessKey")]string AccessKey)
        {
            //if(AccessKey != "123456789") return Unauthorized();
            return Ok(await mediator.Send(new DeleteSiteByIdCommand { Id = id }));
        }
    }
}