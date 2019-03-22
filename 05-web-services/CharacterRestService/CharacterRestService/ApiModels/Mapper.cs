using CharacterRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterRestService.ApiModels
{
    public class Mapper : IMapper
    {
        public ApiCharacter Map(Character character) => new ApiCharacter
        {
            Id = character.Id,
            Name = character.Name
        };

        public Character Map(ApiCharacter apiCharacter) => new Character
        {
            Id = apiCharacter.Id,
            Name = apiCharacter.Name
        };
    }
}
