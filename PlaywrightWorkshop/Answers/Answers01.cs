using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Answers
{
    [TestFixture]
    public class Answers01 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            // Set the default timeout to 5000 milliseconds
            Page.SetDefaultTimeout(5000);
        }

        [Test]
        public async Task UserCanLoginToSauceDemoShop()
        {
            // Navigate to https://www.saucedemo.com
            await Page.GotoAsync("https://www.saucedemo.com");

            // Login by:
            // Typing 'standard_user' into the text field with placeholder 'Username'. Use the GetByPlaceholder() locator.
            // Typing 'secret_sauce' into the text field with placeholder 'Password'.
            // Clicking the button with name 'Login'. See the example for an idea on how to do this.
            await Page.GetByPlaceholder("Username").FillAsync("standard_user");
            await Page.GetByPlaceholder("Password").FillAsync("secret_sauce");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Login"}).ClickAsync();

            // Check that the element with text 'Products' is visible
            // See https://playwright.dev/dotnet/docs/locators#locate-by-text for a hint.
            await Expect(Page.GetByText("Products")).ToBeVisibleAsync();
        }
    }
}
