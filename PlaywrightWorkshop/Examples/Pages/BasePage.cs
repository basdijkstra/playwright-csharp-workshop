using Microsoft.Playwright;

namespace PlaywrightWorkshop.Examples.Pages
{
    public class BasePage
    {
        private readonly IPage page;

        protected BasePage(IPage page)
        {
            this.page = page;
        }

        protected async Task SelectMenuItem(string menuItem)
        {
            await this.page.GetByRole(AriaRole.Link, new() { Name = menuItem }).ClickAsync();
        }
    }
}
