using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages.SwagLabs
{
    public class ProductsOverviewPage
    {
        private readonly IPage page;

        public ProductsOverviewPage(IPage page)
        {
            this.page = page;
        }

        // Create a new method to select a product. It should take a single string argument
        //  that contains the name of the product to be selected, and it should click the link
        //  for the supplied product name (hint: use the product name text to find the element).
        public async Task SelectProduct(string productName)
        {
            await page.GetByText(productName).ClickAsync();
        }
    }
}
