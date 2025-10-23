using EaFramework.Config;
using EaFramework.driver;
using Microsoft.Playwright;

namespace PlaywrightDemo
{
    public class Tests: IClassFixture<PlaywrightDriverInitializer>
    {

        private PlaywrightDriver _playwrightDriver;
        private PlaywrightDriverInitializer _playwrightDriverInitializer;

        public Tests(PlaywrightDriverInitializer playwrightDriverInitializer)
        {
            TestSettings testSettings = new TestSettings
            {
                DevTools = true,
                Headless = false,
                SlowMo = 100,
                DriverType = DriverType.Chrome
            };

            _playwrightDriverInitializer = playwrightDriverInitializer;
            _playwrightDriver = new PlaywrightDriver(testSettings, _playwrightDriverInitializer);
        }

        [Fact]
        public async Task LoginTest()
        {
            var page = await _playwrightDriver.Page;
            await page.GotoAsync("http://eaapp.somee.com");
            await page.ClickAsync("text=Login");
            await page.GetByLabel("UserName").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");

            await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
        }

    }
}