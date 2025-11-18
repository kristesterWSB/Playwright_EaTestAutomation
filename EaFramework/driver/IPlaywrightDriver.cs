using Microsoft.Playwright;

namespace EaFramework.driver
{
    public interface IPlaywrightDriver
    {
        Task<IPage> Page {  get; }
        Task<IBrowser> Browser { get; }
        Task<IBrowserContext> BrowserContext { get; }
    }
}
