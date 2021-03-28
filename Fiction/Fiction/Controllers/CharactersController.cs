using Fiction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fiction.Controllers
{
    [Route("[controller]")]
    public class CharactersController : Controller
    {
        private readonly ICharactersRepository _charactersRepository;

        public CharactersController(ICharactersRepository charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }

        // DZ 4.
        // http://localhost:55795/characters?name=Finn%20Mertens&age=14
        // http://localhost:55795/characters/Finn%20Mertens/14
        [HttpGet]
        [Route("{name?}/{age?}")]
        public IActionResult Index(string name, int? age)
        {
            var characters = _charactersRepository.GetCharactersWithDependencies();

            if (name != null && age != null)
            {
                ViewData["Character"] = characters.FirstOrDefault(character => character.Name == name && character.Age == age);
            }

            else 
            {
                ViewData["Characters"] = characters.ToList();
            }
            return View();
        }

        // DZ 1. Filter all characters with their dependencies by id
        // DZ 3. Path Route parameter via route attributes
        [Route("c/index")]
        [Route("[action]/{index}")]
        public IActionResult Get(int index)
        {
            var characters = _charactersRepository.GetCharactersWithDependencies();
            ViewData["Character"] = characters.FirstOrDefault(character => character.Id == index);

            return View();
        }

        [Route("[action]")]
        public IActionResult Table()
        {
            var characters = _charactersRepository.GetCharactersWithDependencies();
            ViewData["Characters"] = characters.ToList();

            return View();
        }

    }
}
