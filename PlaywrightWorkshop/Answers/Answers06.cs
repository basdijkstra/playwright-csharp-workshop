using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Answers
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

            var headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
            };

            this.apiContext = await this.Playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "https://parabank.parasoft.com",
                ExtraHTTPHeaders = headers
            });
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

            var response = await this.apiContext.PostAsync("/parabank/services/bank/initializeDB");
            Assert.That(response.Status, Is.EqualTo(204));

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

        [TearDown]
        public async Task CleanUp()
        {
            await this.apiContext.DisposeAsync();
        }
    }
}
