using System;
using System.Threading.Tasks;
using AP.MyTreeFarm.Application.CQRS.Zones;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP.MyTreeFarm.WebAPI.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class ZoneController : APIv1Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ZoneController(IMediator mediator,IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]   //api/people?lastname=Janssens
        public async Task<IActionResult> GetAllZones()
        {
            return Ok(await this.mediator.Send(new GetAllZonesQuery())); 
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetZone(int id)
        {
            var zone = await mediator.Send(new GetZoneByIdQuery() { Id = id });
            if (zone == null)
                return NotFound(); 

            return Ok(zone);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateZone([FromBody]CreateZoneCommand zone)
        {

            var result = await mediator.Send(zone);

            if (result.Item2.Count > 0)
            {
                return BadRequest(result);

            }

            return Created("", result.Item1);
        }
        
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateZone(int id, [FromBody]UpdateZoneCommand updatedZone)
        {
            updatedZone.Id = id;
            var result = await mediator.Send(updatedZone);
            var result2 = Tuple.Create(mapper.Map<UpdateZoneDTO>(result.Item1), result.Item2);
            if (result.Item2.Count > 0)
            {
                return BadRequest(result2);

            }
            return Ok(await mediator.Send(updatedZone));
        }
        [Route("{id}")]  //api/people/id
        [HttpDelete]
        public async Task <IActionResult> DeleteZone (int id, [FromHeader(Name ="X-AccessKey")]string AccessKey)
        {
            //if(AccessKey != "123456789") return Unauthorized();
            return Ok(await mediator.Send(new DeleteZoneByIdCommand { Id = id }));
        }
    }
}