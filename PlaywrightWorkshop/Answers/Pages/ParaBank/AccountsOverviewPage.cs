using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages.ParaBank
{
    public class AccountsOverviewPage
    {
        private readonly IPage page;

        public AccountsOverviewPage(IPage page)
        {
            this.page = page;
        }

        public async Task Open()
        {
            await this.page.GotoAsync("https://parabank.parasoft.com/parabank/overview.htm");
        }

        public async Task SelectMenuItem(string menuItem)
        {
            await this.page.GetByRole(AriaRole.Link, new() { Name = menuItem }).ClickAsync();
        }
    }
}
