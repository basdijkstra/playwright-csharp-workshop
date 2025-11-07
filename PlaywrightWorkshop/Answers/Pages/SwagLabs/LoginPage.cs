using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages.SwagLabs
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
            textfieldUsername = this.page.GetByPlaceholder("Username");
            textfieldPassword = this.page.GetByPlaceholder("Password");
            buttonDoLogin = this.page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        }

        public async Task Open()
        {
            await page.GotoAsync("https://www.saucedemo.com");
        }

        public async Task LoginAs(string username, string password)
        {
            await textfieldUsername.FillAsync(username);
            await textfieldPassword.FillAsync(password);
            await buttonDoLogin.ClickAsync();

        }
    }
}
