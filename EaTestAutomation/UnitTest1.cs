using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.driver;
using Microsoft.Playwright;

namespace PlaywrightDemo
{
    public class Tests: IClassFixture<PlaywrightDriverInitializer>
    {

        private PlaywrightDriver _playwrightDriver;
        private PlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly TestSettings _testSettings;

        public Tests(PlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = ConfigReader.ReadConfig();
            _playwrightDriverInitializer = playwrightDriverInitializer;
            _playwrightDriver = new PlaywrightDriver(_testSettings, _playwrightDriverInitializer);
        }

        [Fact]
        public async Task LoginTest()
        {
            var page = await _playwrightDriver.Page;
            await page.GotoAsync(_testSettings.ApplicationUrl);
            await page.ClickAsync("text=Login");
            await page.GetByLabel("UserName").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");

            await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
        }

        [Fact]
        public async Task AddProductTest()
        {

            var page = await _playwrightDriver.Page;
            await page.GotoAsync("http://localhost:8000/");
            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);


            await productListPage.CreateProductAsync();
            await productPage.CreateProduct("AutoName", "AutoDescription", 2000, "2");
            await productPage.ClickCreate();
            await productListPage.ClickProductFromListAsync("AutoName");

            //Assertion
            var element = productListPage.IsProductCreated("AutoName");
            await Assertions.Expect(element).ToBeVisibleAsync();
            

        }

    }
}