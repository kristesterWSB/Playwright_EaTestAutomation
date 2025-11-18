using EaFramework.Config;
using EaFramework.driver;
using Microsoft.Playwright;
using System.IO.Enumeration;


namespace EaApplicationTest.Fixture
{
    public interface ITestFixtureBase
    {
        Task NavigateToUrl();
        Task TakeScreenShotAsync(string fileName);
    }

    public class TestFixtureBase : ITestFixtureBase
    {
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly TestSettings _testSettings;
        private Task<IPage> _page;

        public TestFixtureBase(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
        {
            _playwrightDriver = playwrightDriver;
            _testSettings = testSettings;
            _page = playwrightDriver.Page;
        }

        public async Task NavigateToUrl()
        {
            await (await _page).GotoAsync(_testSettings.ApplicationUrl);
        }

        public async Task TakeScreenShotAsync(string fileName)
        {
            await (await _page).ScreenshotAsync(new PageScreenshotOptions() { Path = fileName, FullPage = true });
        }


    }
}
