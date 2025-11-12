using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises01 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            // Set the default timeout to 5000 milliseconds
        }

        [Test]
        public async Task UserCanLoginToSauceDemoShop()
        {
            // Navigate to https://www.saucedemo.com

            // Login by:
            // Typing 'standard_user' into the text field with placeholder 'Username'. Use the GetByPlaceholder() locator.
            // Typing 'secret_sauce' into the text field with placeholder 'Password'.
            // Clicking the button with name 'Login'. See the example for an idea on how to do this.

            // Check that the element with text 'Products' is visible
            // See https://playwright.dev/dotnet/docs/locators#locate-by-text for a hint.
            
        }
    }
}
