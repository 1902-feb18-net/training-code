using CharacterRest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRestDAL
{
    // here as well as in the app db context i am
    // using my domain/business logic models as EF's entities directly.
    // this is more challenging with more complex classes than Character,
    // in which case you may want to have separate models and then map between them.
    public class CharacterRepository : ICharacterRepository
    {
        private readonly AppDbContext _dbContext;

        public CharacterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Character> GetAll()
        {
            return _dbContext.Characters;
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            return await _dbContext.Characters.FindAsync(id);
        }

        public async Task<Character> AddAsync(Character character)
        {
            _dbContext.Add(character);
            await _dbContext.SaveChangesAsync();

            return character;
        }

        public async Task UpdateAsync(Character character)
        {
            _dbContext.Entry(character).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Character character)
        {
            _dbContext.Remove(character);
            await _dbContext.SaveChangesAsync();
        }
    }
}
