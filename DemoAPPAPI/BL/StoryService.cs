using DemoAPPAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DemoAPPAPI.BL
{
    public class StoryService: IStoryService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpHelper _httpHelper;
        public string cacheKey = "story";

        public StoryService(IMemoryCache memoryCache, IHttpHelper httpHelper)
        {

            _memoryCache = memoryCache;
            _httpHelper = httpHelper;
        }
        public async Task< List<Story>> GetTopStories()
        {
            try
            {
                if (!_memoryCache.TryGetValue(cacheKey, out List<Story> stories)) // apply caching here
                {
                    stories = new List<Story>();
                    var storyNumber = await this._httpHelper.Get<List<string>>("v0/newstories.json?print=pretty"); // getting story numbers

                    storyNumber = storyNumber.Take(200).ToList();
                    await Parallel.ForEachAsync(storyNumber, async (i, token) =>
                    {
                        var story = await this._httpHelper.Get<Story>("v0/item/" + i + ".json?print=pretty"); // calling storing data by story id
                        if (!string.IsNullOrEmpty(story.Url))
                            stories.Add(story);
                    });

                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        SlidingExpiration = TimeSpan.FromMinutes(2),
                        Size = 1024,
                    };

                    _memoryCache.Set(cacheKey, stories, cacheEntryOptions);
                }

                return stories;
            }
            catch (Exception ex)
            {
                return new List<Story>();
            }

        }
    }
}
