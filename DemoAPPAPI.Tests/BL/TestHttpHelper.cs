using DemoAPPAPI.BL;
using DemoAPPAPI.Controllers;
using DemoAPPAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPPAPI.Tests.BL
{
    public class TestHttpHelper
    {
        [Fact]
        public async void HttpHelper_Get_ValidateResult()
        {
            //Arrange
            var mockData = new List<Story>();
            string expectedData = "[{ 'by': 'marcodiego', 'descendants': 0, 'id': 40729250, 'score': 1, 'time': 1718811366, 'title': 'MuseBook RISC-V Laptop', 'type': 'story', 'url': 'https://arace.tech' }, { 'by': 'marcodiego', 'descendants': 0, 'id': 40729250, 'score': 1, 'time': 1718811366, 'title': 'MuseBook RISC-V Laptop', 'type': 'story', 'url': 'https://arace.tech' }, { 'by': 'marcodiego', 'descendants': 0, 'id': 40729250, 'score': 1, 'time': 1718811366, 'title': 'MuseBook RISC-V Laptop', 'type': 'story', 'url': 'https://arace.tech' }]";
            mockData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Story>>(expectedData); // prepare mock data


            Mock<IHttpHelper> mockIHttpHelper = new Mock<IHttpHelper>(); // creating IHttpHelper mock
            mockIHttpHelper.Setup(x => x.Get<List<Story>>("test")).ReturnsAsync(mockData);
            IHttpHelper httpHelper = mockIHttpHelper.Object;

            //Act
            List<Story> Result = await httpHelper.Get<List<Story>>("test");

            //Assert
            Assert.Equal(mockData, Result);
        }
    }
}
