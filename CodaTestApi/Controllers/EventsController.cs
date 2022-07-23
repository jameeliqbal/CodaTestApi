using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodaTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        // GET: /Events
        [HttpGet]
        public IActionResult GetAll()
        {
            return new  JsonResult  (new string[] { "value1", "value2" });
        }

        // GET /Events/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("value");
        }

        // POST /Events
        [HttpPost]
        public IActionResult Create([FromBody] string value)
        {
        }

        // PUT /Events/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string value)
        {
        }

        // DELETE /Events/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
        }
    }
}
