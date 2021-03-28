using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fiction.Models
{
    public class SQLCharactersRepository : ICharactersRepository
    {
        private readonly FictionDbContext _dbContext;

        public SQLCharactersRepository(FictionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Character> GetCharacters()
        {
            return _dbContext.Characters;
        }

        // DZ 1. Get all characters with all their dependencies
        public IEnumerable<Character> GetCharactersWithDependencies()
        {
            return _dbContext.Characters.Include(character => character.Story);
        }
    }
}
