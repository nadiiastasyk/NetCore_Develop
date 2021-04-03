using System.Collections.Generic;

namespace Fiction_DZ6.Models
{
    public interface ICharactersRepository
    {
        IEnumerable<Character> GetCharacters();

        IEnumerable<Character> GetCharactersWithDependencies();

        void Add(Character character);
    }
}
