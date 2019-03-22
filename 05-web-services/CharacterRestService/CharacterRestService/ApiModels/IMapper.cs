using CharacterRest;

namespace CharacterRestService.ApiModels
{
    public interface IMapper
    {
        ApiCharacter Map(Character character);
        Character Map(ApiCharacter apiCharacter);
    }
}