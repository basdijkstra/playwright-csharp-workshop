using Microsoft.Playwright;

namespace PlaywrightWorkshop.Examples.Pages
{
    public class LoginPage
    {
        private readonly IPage page;

        private readonly ILocator textfieldUsername;
        private readonly ILocator textfieldPassword;
        private readonly ILocator buttonDoLogin;

        public LoginPage(IPage page)
        {
            this.page = page;
            this.textfieldUsername = this.page.Locator("xpath=//input[@name='username']");
            this.textfieldPassword = this.page.Locator("xpath=//input[@name='password']");
            this.buttonDoLogin = this.page.GetByRole(AriaRole.Button, new() { Name = "Log In" });
        }

        public async Task Open()
        {
            await this.page.GotoAsync("https://parabank.parasoft.com");
        }

        public async Task LoginAs(string username, string password)
        {
            await this.textfieldUsername.FillAsync(username);
            await this.textfieldPassword.FillAsync(password);
            await this.buttonDoLogin.ClickAsync();
        }
    }
}
