using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CharacterRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly static Dictionary<int, string> _data = new Dictionary<int, string>()
        {
            [1] = "value1",
            [2] = "value2"
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _data.Values;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<string> Get(int id)
        {
            if (!_data.ContainsKey(id))
            {
                return NotFound();
            }

            return _data[id];
        }

        // POST api/values
        // POST method is for creating resources
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            var newId = _data.Keys.Max() + 1;
            _data[newId] = value;
            // proper REST, from a POST we return the resource, plus where it can be found.
            return CreatedAtRoute("GetById", new { id = newId }, value);
        }

        // PUT api/values/5
        // PUT is for replacing/updating resources that already exist.
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _data[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _data.Remove(id);
        }
    }
}
