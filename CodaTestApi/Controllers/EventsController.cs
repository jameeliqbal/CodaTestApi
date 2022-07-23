using AutoMapper;
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
            return Ok(events);
        }

        // GET /Events/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var @event = eventService.GetById(id);
            return Ok(@event);

        }

        // POST /Events
        [HttpPost]
        public IActionResult Create(CreateEventRequest model)
        {
            eventService.Create(model);
            return Ok(new { message = "Event Created!" });
        }

        // PUT /Events/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateEventRequest model)
        {
            eventService.Update(id,model);
            return Ok(new { message = "Event Updated!" });
        }

        // DELETE /Events/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            eventService.Delete(id);
            return Ok(new { message = "Event Deleted!" });
        }
    }
}
