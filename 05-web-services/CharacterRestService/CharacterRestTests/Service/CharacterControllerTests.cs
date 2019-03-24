using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterRest;
using CharacterRestService.ApiModels;
using CharacterRestService.Controllers;
using Moq;
using Xunit;

namespace CharacterRestTests.Service
{
    public class CharacterControllerTests
    {
        [Fact]
        public void GetWithCharsShouldGetThem()
        {
            var characters = new List<Character>
            {
                new Character { Id = 1, Name = "Nick" },
                new Character { Id = 2, Name = "Fred" }
            };

            var mockRepo = new Mock<ICharacterRepository>();
            mockRepo.Setup(x => x.GetAll()).Returns(characters);

            var mapper = new Mapper();

            var sut = new CharacterController(mockRepo.Object, mapper);

            IEnumerable<ApiCharacter> result = sut.Get();

            var resultList = result.ToList();

            Assert.Equal(characters.Count, resultList.Count);
            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.Equal(characters[i].Id, resultList[i].Id);
                Assert.Equal(characters[i].Name, resultList[i].Name);
            }
        }
    }
}
