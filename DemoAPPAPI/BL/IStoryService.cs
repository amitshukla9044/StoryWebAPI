using DemoAPPAPI.Models;

namespace DemoAPPAPI.BL
{
    public interface IStoryService
    {
         Task<List<Story>> GetTopStories();
    }
}
