using AutoMapper;
using CodaTestApi.Entities;
using CodaTestApi.Helpers;
using CodaTestApi.Models.Events;
using CodaTestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private IEventService eventService;
        private IMapper mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }

        // GET: /Events
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            //var events = eventService.GetAll(filter);
            //return Ok(new Response<IEnumerable<Event>>( events));
            var route = Request.Path.Value;
            var eventsPagedData = await eventService.GetAll(filter, route).ConfigureAwait(false);
            
            return Ok(eventsPagedData);
        }

        // GET /Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var @event = await eventService.GetById(id);
            return Ok(new Response<Event>(@event));

        }

        // POST /Events
        [HttpPost]
        public async Task<IActionResult> Create(CreateEventRequest model)
        {
           var newEvent= await  eventService.Create(model).ConfigureAwait(false);
            // return Ok(new { message = "Event Created!" });
            return Ok(new Response<Event>(newEvent) { Message = "Event Created!" });
        }

        // PUT /Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateEventRequest model)
        {
           await  eventService.Update(id,model).ConfigureAwait(false);
            //return Ok(new { message = "Event Updated!" });
            return Ok(new Response<Event>() { Succeeded=true, Message = "Event Updated!" });
        }

        // DELETE /Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await eventService.Delete(id).ConfigureAwait(false);
            // return Ok(new { message = "Event Deleted!" });
            return Ok(new Response<int>() { Succeeded = true, Data =id, Message = "Event Deleted!" });

        }
    }
}
