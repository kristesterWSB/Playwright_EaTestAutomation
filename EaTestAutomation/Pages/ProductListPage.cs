using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaApplicationTest.Pages
{
    public class ProductListPage
    {
        private readonly IPage _page;

        public ProductListPage(IPage page)
        {
            _page = page;
        }

        private ILocator _lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
        private ILocator _lnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });

        private ILocator _row => _page.GetByRole(AriaRole.Row, new() { Name = "AutoName" })
            .GetByRole(AriaRole.Link, new() { Name = "Details" });


        public async Task CreateProductAsync()
        {
            await _lnkProductList.ClickAsync();
            await _lnkCreate.ClickAsync();
        }

        public async Task ClickProductFromListAsync(string name)
        {
            await _page.GetByRole(AriaRole.Row, new() { Name = name })
             .GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
        }

        public ILocator IsProductCreated(string product)
        {
            return _page.GetByText(product, new() { Exact = true });
        }

    }        
}
