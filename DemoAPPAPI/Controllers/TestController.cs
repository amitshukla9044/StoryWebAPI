using DemoAPPAPI.BL;
using DemoAPPAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace DemoAPPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
       
        private readonly IStoryService _storyService;
       

        public TestController(IStoryService storyService)
        {

            _storyService = storyService;
        }


        [HttpGet]
        [Route("GetTopStories")]
        public async Task<List<Story>> GetTopStories()
        {
            try {
                return await this._storyService.GetTopStories();
            }
            catch (Exception ex)
            {
                return new List<Story>();
            }
            


        }





    }
}
