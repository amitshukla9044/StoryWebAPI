using DemoAPPAPI.BL;
using DemoAPPAPI.Controllers;
using DemoAPPAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPPAPI.Tests.Controllers
{
    public class TestControllerTest
    {
        // private readonly IMemoryCache _memoryCache;

        //public TestControllerTest(IMemoryCache memoryCache)
        //{

        //    _memoryCache = memoryCache;
        //}
        /// <summary>
        /// GetStoryDetails function must return a value when passes parameter StoryId
        /// </summary>
        [Fact]
        public async void TestController_GetTopStories_ValidResult()
        {
            //Arrange
            var mockData = new List<Story>();
            string expectedData = "[{ 'by': 'marcodiego', 'descendants': 0, 'id': 40729250, 'score': 1, 'time': 1718811366, 'title': 'MuseBook RISC-V Laptop', 'type': 'story', 'url': 'https://arace.tech' }, { 'by': 'marcodiego', 'descendants': 0, 'id': 40729250, 'score': 1, 'time': 1718811366, 'title': 'MuseBook RISC-V Laptop', 'type': 'story', 'url': 'https://arace.tech' }, { 'by': 'marcodiego', 'descendants': 0, 'id': 40729250, 'score': 1, 'time': 1718811366, 'title': 'MuseBook RISC-V Laptop', 'type': 'story', 'url': 'https://arace.tech' }]";
            mockData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Story>>(expectedData); // prepare mock data
            
            Mock<IStoryService> mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(x => x.GetTopStories()).ReturnsAsync(mockData);
            IStoryService storyService = mockStoryService.Object;
            TestController testController = new TestController(storyService);

            //Act
            List<Story> Result = await testController.GetTopStories();

            //Assert
            Assert.Equal(mockData, Result);

        }
        [Fact]
        public async void TestController_GetTopStories_ValidResultWithEmptyListOrGettingError()
        {
            var mockData = new List<Story>(); // when list is not containing any value
            
            Mock<IStoryService> mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(x => x.GetTopStories()).ReturnsAsync(mockData);
            IStoryService storyService = mockStoryService.Object;
            TestController testController= new TestController(storyService);

            List<Story> Result = await testController.GetTopStories();

            Assert.Equal(mockData, Result);

        }
    }
}
