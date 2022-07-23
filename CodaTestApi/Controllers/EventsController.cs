using AutoMapper;
using CodaTestApi.Entities;
using CodaTestApi.Helpers;
using CodaTestApi.Models.Events;
using CodaTestApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult GetAll()
        {
            var events = eventService.GetAll();
            return Ok(new Response<IEnumerable<Event>>( events));
        }

        // GET /Events/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var @event = eventService.GetById(id);
            return Ok(new Response<Event>(@event));

        }

        // POST /Events
        [HttpPost]
        public IActionResult Create(CreateEventRequest model)
        {
           var newEvent=  eventService.Create(model);
            // return Ok(new { message = "Event Created!" });
            return Ok(new Response<Event>(newEvent) { Message = "Event Created!" });
        }

        // PUT /Events/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateEventRequest model)
        {
            eventService.Update(id,model);
            //return Ok(new { message = "Event Updated!" });
            return Ok(new Response<Event>() { Succeeded=true, Message = "Event Updated!" });
        }

        // DELETE /Events/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            eventService.Delete(id);
            // return Ok(new { message = "Event Deleted!" });
            return Ok(new Response<int>() { Succeeded = true, Data =id, Message = "Event Deleted!" });

        }
    }
}
