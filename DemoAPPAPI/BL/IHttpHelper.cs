namespace DemoAPPAPI.BL
{
    public interface IHttpHelper
    {
        Task<T> Get<T>(string url);
    }
}
