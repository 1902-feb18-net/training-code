using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRest
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> GetAll();
        Task<Character> GetByIdAsync(int id);
        Task<Character> AddAsync(Character character);
        Task UpdateAsync(Character character);
        Task DeleteAsync(Character character);
    }
}
