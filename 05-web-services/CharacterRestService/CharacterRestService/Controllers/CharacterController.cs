using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharacterRestService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharacterRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static readonly List<Character> _data = new List<Character>()
        {
            new Character { Id = 1, Name = "Nick" },
            new Character { Id = 2, Name = "Fred" },
        };

        // GET: api/Character
        [HttpGet]
        public IEnumerable<Character> Get()
        {
            return _data;
        }

        // POST: api/Character
        [HttpPost]
        public void Post([FromBody] Character character)
        {
            var newId = _data.Max(x => x.Id) + 1;
            character.Id = newId;
            _data.Add(character);
        }
    }
}
