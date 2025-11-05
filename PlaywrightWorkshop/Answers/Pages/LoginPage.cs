using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages
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
            this.textfieldUsername = this.page.GetByPlaceholder("Username");
            this.textfieldPassword = this.page.GetByPlaceholder("Password");
            this.buttonDoLogin = this.page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        }

        public async Task Open()
        {
            await this.page.GotoAsync("https://www.saucedemo.com");
        }

        public async Task LoginAs(string username, string password)
        {
            await this.textfieldUsername.FillAsync(username);
            await this.textfieldPassword.FillAsync(password);
            await this.buttonDoLogin.ClickAsync();

        }
    }
}
