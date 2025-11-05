using Microsoft.Playwright;

namespace PlaywrightWorkshop.Examples.Pages
{
    public class AccountsOverviewPage
    {
        private readonly IPage page;

        public AccountsOverviewPage(IPage page)
        {
            this.page = page;
        }

        public async Task SelectMenuItem(string menuItem)
        {
            await this.page.GetByRole(AriaRole.Link, new() { Name = menuItem }).ClickAsync();
        }
    }
}
