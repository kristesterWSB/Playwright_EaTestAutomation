using AutoFixture.Xunit3;
using EaApplicationTest.Fixture;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using Microsoft.Playwright;

namespace PlaywrightDemo
{
    public class CreateProductTest
    {

        private readonly IProductPage _productPage;
        private readonly IProductListPage _productListPage;
        private readonly ITestFixtureBase _testFixtureBase;

        public CreateProductTest(ITestFixtureBase testFixtureBase, IProductPage productPage, IProductListPage productListPage)
        {
            _productPage = productPage;
            _productListPage = productListPage;
            _testFixtureBase = testFixtureBase;
        }

        [Theory, AutoData]
        public async Task AddProductTestWithAutoFixture(Product product)
        {
            //Arrange
            await _testFixtureBase.NavigateToUrl();

            //Act
            await _productListPage.CreateProductAsync();
            await _productPage.CreateProduct(product);
            await _productPage.ClickCreate();
            await _productListPage.ClickProductFromListAsync(product.Name);

            //Asser
            var element = _productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

    }
}