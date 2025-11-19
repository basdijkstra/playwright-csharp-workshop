using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises05 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            Page.SetDefaultTimeout(5000);
        }

        /**
         * TODO: Refactor these two tests into a single, parameterized test
         * Which values differ from one test to the other? Pass those in as parameters
         * These can be input values, but also expected output values
         */

        [Test]
        public async Task LoanApplicationFor10000_WithDownPaymentOf1000_IsDenied()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page"}).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "INIT" }).ClickAsync();
            await Expect(Page.GetByText("Database Initialized")).ToBeVisibleAsync();

            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");
                        
            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("10000", "1000", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Denied");
        }

        [Test]
        public async Task LoanApplicationFor1000_WithDownPaymentOf500_IsApproved()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "INIT" }).ClickAsync();
            await Expect(Page.GetByText("Database Initialized")).ToBeVisibleAsync();

            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");

            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("1000", "500", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Approved");
        }
    }
}
