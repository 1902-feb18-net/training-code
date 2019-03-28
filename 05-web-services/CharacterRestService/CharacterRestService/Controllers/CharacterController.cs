using CharacterRest;
using CharacterRestService.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterRestService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        // GET: api/Character
        [HttpGet]
        [AllowAnonymous]
        //[Authorize]
        //[Produces("application/xml")]
        public IEnumerable<ApiCharacter> Get()
        {
            return _characterRepository.GetAll().Select(_mapper.Map);
            // whenever an action method returns something that's not an IActionResult
            // ... it's automatically wrapped in 200 OK response.
        }


        // GET: api/Character/1
        [HttpGet("{id}")]
        [Authorize] // Authorize by itself means, any logged in user (anyone authenticated)
                    // can access this action method.
                    //[Produces("application/xml")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiCharacter>> GetById(int id)
        {
            // using fancy pattern matching syntax
            if (await _characterRepository.GetByIdAsync(id) is Character character)
            {
                return _mapper.Map(character);
            }

            return NotFound();
        }

        // return types should generally either be IActionResult or some subtype of that.
        // or ActionResult<type-of-data-in-body>

        // POST: api/Character
        [HttpPost]
        [Authorize(Roles = "admin")] // we give comma-separated list of allowed roles
        [ProducesResponseType(typeof(ApiCharacter), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody, Bind("Name")] ApiCharacter apiCharacter)
        {
            // there was some "overposting" vulnerability here... i do not want client to be able
            // to set the ID. either i can make sure to ignore that value if he sends it
            // or i can explicitly not bind it.

            Character character = _mapper.Map(apiCharacter);
            Character newCharacter = await _characterRepository.AddAsync(character);
            ApiCharacter newApiCharacter = _mapper.Map(newCharacter);
            return CreatedAtAction(nameof(GetById), new { id = newApiCharacter.Id },
                newApiCharacter);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody, Bind("Name")] ApiCharacter apiCharacter)
        {
            // implementation choice, whether PUT on nonexistent resource is
            // successful or error.
            if (await _characterRepository.GetByIdAsync(id) is null)
            {
                return NotFound();
            }
            Character character = _mapper.Map(apiCharacter);
            character.Id = id;
            await _characterRepository.UpdateAsync(character);

            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _characterRepository.GetByIdAsync(id) is Character character)
            {
                await _characterRepository.DeleteAsync(character);
                return NoContent(); // 204
            }
            return NotFound();
        }
    }
}
