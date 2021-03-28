using System.Collections.Generic;

namespace Fiction.Models
{
    public interface ICharactersRepository
    {
        IEnumerable<Character> GetCharacters();

        IEnumerable<Character> GetCharactersWithDependencies();
    }
}
