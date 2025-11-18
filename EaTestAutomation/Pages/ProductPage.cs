using EaApplicationTest.Models;
using EaFramework.driver;
using Microsoft.Playwright;


namespace EaApplicationTest.Pages
{
    public interface IProductPage
    {
        Task ClickCreate();
        Task CreateProduct(Product product);
    }

    public class ProductPage : IProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

        private ILocator _txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        private ILocator _txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        private ILocator _txtPrice => _page.Locator("#Price");
        private ILocator _SelectProduct => _page.GetByLabel("ProductType");
        private ILocator _lnkCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task CreateProduct(Product product)
        {
            await _txtName.FillAsync(product.Name);
            await _txtDescription.FillAsync(product.Description);
            await _txtPrice.FillAsync(product.Price.ToString());
            await _SelectProduct.SelectOptionAsync(product.ProductType.ToString());
        }

        public async Task ClickCreate() => await _lnkCreate.ClickAsync();

    }
}
