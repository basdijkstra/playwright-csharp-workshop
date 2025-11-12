using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises06 : PageTest
    {
        private IAPIRequestContext apiContext;

        [SetUp]
        public async Task SetUp()
        {
            Page.SetDefaultTimeout(5000);

            /**
             * TODO: Initialize the apiContext object
             * Use 'https://parabank.parasoft.com' for the BaseURL
             * Add an Accept header with the value 'application/json'
             */

        }

        [TestCase("10000", "1000", "12345", "Denied", TestName = "Loan application for 10000 with 1000 down payment is denied")]
        [TestCase("1000", "500", "12345", "Approved", TestName = "Loan application for 1000 with 500 down payment is approved")]
        public async Task SubmitLoanApplication_CheckResult_ShouldMatchExpected
            (string amount, string downPayment, string fromAccountId, string expectedResult)
        {
            /**
             * TODO: Replace the first four statements with an HTTP POST call to /parabank/services/bank/initializeDB
             * Check that the response status code is equal to 204
             */

            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "INIT" }).ClickAsync();
            await Expect(Page.GetByText("Database Initialized")).ToBeVisibleAsync();

            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");
                        
            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor(amount, downPayment, fromAccountId);

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync(expectedResult);
        }

        /**
         * TODO: Add a [TearDown] method that properly disposes the IAPIRequestContext object
         */
    }
}
