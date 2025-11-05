using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.InstallationCheck
{
    [TestFixture]
    public class InstallationCheckTests : PageTest
    {
        [Test]
        public async Task CheckWhetherEverythingWorks()
        {
            await Page.GotoAsync("https://www.saucedemo.com");

            await Expect(Page).ToHaveTitleAsync("Swag Labs");
        }
    }
}
