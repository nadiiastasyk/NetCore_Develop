using Fiction_DZ6.Controllers;
using Fiction_DZ6.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FictionTests
{
    public class CharacterControllerTests
    {
        [Fact]
        public void Get_DependencyCalledOnce_Success()
        {
            // Arrange
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> { new Character { } });

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act
            sut.Get(0);

            // Assert
            charactersRepository.Verify(x => x.GetCharactersWithDependencies(), Times.Once);

        }

        [Fact]
        public void Get_ByIndex_ViewData_isNotNull()
        {
            // Arrange
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> {
                new Character {
                    Id = 2,
                    Name = "Philip Fry",
                    Age = 25,
                    StoryId = 2 }});

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act
            var character = sut.Get(2);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(character);

            Assert.Null(viewResult.ViewData.Model);
            Assert.NotNull(viewResult.ViewData["Character"]);
        }

        [Fact]
        public void Get_ByIndex_NotExisting_ExceptionIsThrown()
        {
            // Arrange
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> {
                new Character {
                    Id = 2,
                    Name = "Philip Fry",
                    Age = 25,
                    StoryId = 2 }});

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act
            try
            {
                var character = sut.Get(6);
            }

            // Assert
            catch (InvalidOperationException ex)
            {
                Assert.Equal("Sequence contains no matching element", ex.Message);
            }
        }

        [Fact]
        public void Index_DependencyCalledOnce_Success()
        {
            // Arrange. Mock Character with a name
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();

            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> { new Character { } });
            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Get Character 
            var character = sut.Index(null, null);

            // Assert. Verify the single character with a specified Name is returned
            charactersRepository.Verify(x => x.GetCharactersWithDependencies(), Times.Once);
        }

        [Fact]
        public void Index_GetCharacterByNameOnly_ViewData_IsNull()
        {
            // Arrange. Mock Character
            string name = "Character_" + Guid.NewGuid();
            int? age = null;
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> {
                new Character
                {
                    Name = name
                }});

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Get Character by Name
            IActionResult character = sut.Index(name, age);

            // Assert. ViewData is not returned.
            ViewResult viewResult = Assert.IsType<ViewResult>(character);

            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewData["Character"]);
        }

        [Fact]
        public void Index_GetCharacterByAgeOnly_ViewData_isNull()
        {
            // Arrange. Mock Character with an age
            string name = null;
            const int age = 45;
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> {
                new Character
                {
                    Age = age
                }});

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Get Character by age
            IActionResult character = sut.Index(name, age);

            // Assert. ViewData is not returned.
            ViewResult viewResult = Assert.IsType<ViewResult>(character);

            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewData["Character"]);
        }

        [Fact]
        public void Index_GetCharacterByNameAndAge_ViewData_isNotNull()
        {
            // Arrange. Mock Character with a name and age
            string name = "Character" + Guid.NewGuid();
            const int age = 34;
            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharactersWithDependencies()).Returns(new List<Character> {
                new Character
                {
                    Name = name,
                    Age = age
                }});

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Get Character by Name and Age
            IActionResult character = sut.Index(name, age);

            // Assert. Verify ViewData is returned
            ViewResult viewResult = Assert.IsType<ViewResult>(character);

            Assert.Null(viewResult.ViewData.Model);
            Assert.NotNull(viewResult.ViewData["Character"]);
        }

        [Fact]
        public void Add_DependencyCalledOnce_Success()
        {
            // Arrange. Mock Character 
            var character = new Character
            {
                Name = "Character_" + Guid.NewGuid(),
                Age = 34,
                Id = 2
            };

            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Add Character 
            IActionResult result = sut.Add(character);

            // Assert. Verify Add action is called once
            charactersRepository.Verify(x => x.Add(character), Times.Once);
        }

        [Fact]
        public void Add_Character_isRedirected()
        {
            // Arrange. Mock Character 
            var character = new Character
            {
                Name = "Character_" + Guid.NewGuid(),
                Age = 34,
                Id = 2
            };

            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
     
            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Add Character 
            IActionResult result = sut.Add(character);

            // Assert. Verify RedirectToActionResult is returned
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Add_Character_Duplicate_Fail()
        {
            // Arrange. Mock Character 
            var character = new Character
            {
                Name = "Character_" + Guid.NewGuid(),
                Age = 34,
                Id = 2
            };

            Mock<ICharactersRepository> charactersRepository = new Mock<ICharactersRepository>();
            charactersRepository.Setup(x => x.GetCharacters()).Returns(new List<Character> { character });

            CharactersController sut = new CharactersController(charactersRepository.Object);

            // Act. Add duplicate Character
            IActionResult result = sut.Add(character);

            // Assert. Verify a character is not added.
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }

    }
}
