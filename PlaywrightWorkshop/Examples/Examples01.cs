using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class Examples01 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            Page.SetDefaultTimeout(5000);
        }

        [Test]
        public async Task UserCanLoginToParaBank()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.Locator("xpath=//input[@name='username']").FillAsync("john");
            await Page.Locator("xpath=//input[@name='password']").FillAsync("demo");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();

            await Expect(Page).ToHaveTitleAsync("ParaBank | Accounts Overview");
        }
    }
}
