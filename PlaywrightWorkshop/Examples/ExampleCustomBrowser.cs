using Microsoft.Playwright;
using PlaywrightWorkshop.Examples.Pages;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class ExampleCustomBrowser
    {
        private IPlaywright playwright;
        private IPage page;

        [SetUp]
        public async Task LaunchInstalledBrowser()
        {
            // Create a new playwright, browser, context and page
            this.playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                Headless = false
            });
            var context = await browser.NewContextAsync();
            this.page = await context.NewPageAsync();
        }

        [Test]
        public async Task UserCanSubmitALoanApplicationAndSeeTheResult_UsingPageObjects()
        {
            // Log in to ParaBank
            var loginPage = new LoginPage(this.page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            var accountsOverviewPage = new AccountsOverviewPage(this.page);
            await accountsOverviewPage.Open();
            await accountsOverviewPage.SelectMenuItem("Request Loan");

            var requestLoanPage = new RequestLoanPage(this.page);
            await requestLoanPage.SubmitLoanRequestFor("1000", "100", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Denied");
        }

        [TearDown]
        public async Task CleanUp()
        {
            this.playwright.Dispose();
        }
    }
}
