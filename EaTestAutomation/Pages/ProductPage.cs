using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaApplicationTest.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPage page)
        {
            _page = page;
        }

        private ILocator _txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        private ILocator _txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        private ILocator _txtPrice => _page.Locator("#Price");
        private ILocator _SelectProduct => _page.GetByLabel("ProductType");
        private ILocator _lnkCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task CreateProduct(string name,  string description, decimal price, string productType)
        {
            await _txtName.FillAsync(name);
            await _txtDescription.FillAsync(description);
            await _txtPrice.FillAsync(price.ToString());
            await _SelectProduct.SelectOptionAsync(productType);
        }

        public async Task ClickCreate() => await _lnkCreate.ClickAsync();

    }
}
