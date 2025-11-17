using AutoFixture.Xunit3;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.driver;
using Microsoft.Playwright;

namespace PlaywrightDemo
{
    public class Tests
    {

        private readonly PlaywrightDriver _playwrightDriver;
        private readonly TestSettings _testSettings;

        public Tests(IPlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = ConfigReader.ReadConfig();
            _playwrightDriver = new PlaywrightDriver(_testSettings, playwrightDriverInitializer);
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

        [Theory]
        [InlineData("AutoName1", "AutoDescription1", 2001, "2")]
        [InlineData("AutoName2", "AutoDescription2", 2002, "3")]
        public async Task AddProductTest_WithInlineData(string name, string description, int price, string productType)
        {

            var page = await _playwrightDriver.Page;
            await page.GotoAsync("http://localhost:8000/");
            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);


            await productListPage.CreateProductAsync();
           // await productPage.CreateProduct(name, description, price, productType);
            await productPage.ClickCreate();
            await productListPage.ClickProductFromListAsync(name);

            //Assertion
            var element = productListPage.IsProductCreated(name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

        [Fact]
        public async Task AddProductTest_WithConcreteTypes()
        {

            var page = await _playwrightDriver.Page;

            var product = new Product()
            {
                Name = "AutoNameModel1",
                Description = "AutoDescriptionModel1",
                Price = 1001,
                ProductType = ProductType.CPU
            };

            await page.GotoAsync("http://localhost:8000/");
            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);


            await productListPage.CreateProductAsync();
            await productPage.CreateProduct(product);
            await productPage.ClickCreate();
            await productListPage.ClickProductFromListAsync(product.Name);

            //Assertion
            var element = productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

        [Theory, AutoData]
        public async Task AddProductTestWithAutoFixture(Product product)
        {

            var page = await _playwrightDriver.Page;

            await page.GotoAsync("http://localhost:8000/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);


            await productListPage.CreateProductAsync();
            await productPage.CreateProduct(product);
            await productPage.ClickCreate();
            await productListPage.ClickProductFromListAsync(product.Name);

            //Assertion
            var element = productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

    }
}