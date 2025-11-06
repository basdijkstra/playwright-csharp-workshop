using Microsoft.Playwright;
using PlaywrightWorkshop.Examples.Pages;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class ExampleStoreBrowserState
    {
        private readonly string pathToBrowserState = @"../../../playwright/.auth/parabank-state.json";

        private IBrowser browser;

        [OneTimeSetUp]
        public async Task LoginAndStoreBrowserState()
        {
            // Create a new playwright, browser, context and page
            var playwright = await Playwright.CreateAsync();
            this.browser = await playwright.Chromium.LaunchAsync();
            var context = await this.browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Log in to ParaBank
            var loginPage = new LoginPage(page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            // Wait until page is loaded
            await Expect(page).ToHaveTitleAsync("ParaBank | Accounts Overview");

            // Store the current browser state
            await context.StorageStateAsync(new()
            {
                Path = this.pathToBrowserState
            });
        }

        [Test]
        public async Task UserCanSubmitALoanApplicationAndSeeTheResult_UsingPageObjects()
        {
            var loggedInContext = await this.browser.NewContextAsync(new()
            {
                StorageStatePath = this.pathToBrowserState
            });

            var loggedInPage = await loggedInContext.NewPageAsync();

            var accountsOverviewPage = new AccountsOverviewPage(loggedInPage);
            await accountsOverviewPage.Open();
            await accountsOverviewPage.SelectMenuItem("Request Loan");

            var requestLoanPage = new RequestLoanPage(loggedInPage);
            await requestLoanPage.SubmitLoanRequestFor("1000", "100", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Approved");
        }
    }
}
