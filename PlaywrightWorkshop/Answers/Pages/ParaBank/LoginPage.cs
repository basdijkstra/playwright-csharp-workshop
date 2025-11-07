using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages.ParaBank
{
    public class LoginPage
    {
        private readonly string url = "https://parabank.parasoft.com";

        private readonly IPage page;
        private readonly ILocator textfieldUsername;
        private readonly ILocator textfieldPassword;
        private readonly ILocator buttonDoLogin;

        public LoginPage(IPage page)
        {
            this.page = page;
            textfieldUsername = this.page.Locator("input[name=username]");
            textfieldPassword = this.page.Locator("input[name=password]");
            buttonDoLogin = this.page.GetByRole(AriaRole.Button, new() { Name = "Log In" });
        }

        public async Task Open()
        {
            await page.GotoAsync(url);
        }

        public async Task LoginAs(string username, string password)
        {
            await textfieldUsername.FillAsync(username);
            await textfieldPassword.FillAsync(password);
            await buttonDoLogin.ClickAsync();
        }
    }
}
