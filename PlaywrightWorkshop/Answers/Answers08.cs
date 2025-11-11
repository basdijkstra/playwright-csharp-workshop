using Microsoft.Playwright;
using Microsoft.Playwright.Core;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Answers
{
    [TestFixture]
    public class Answers08 : PageTest
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

        [Test]
        public async Task SubmitLoanApplication_CheckResult_ShouldMatchExpected()
        {
            var response = await this.apiContext.PostAsync("/parabank/services/bank/initializeDB");
            Assert.That(response.Status, Is.EqualTo(204));

            await Page.RouteAsync("*/**/requestLoan?customerId=12212&amount=10000&downPayment=1000&fromAccountId=12345", async route =>
            {
                var mockResponse = new
                {
                    responseDate = 1758177294806,
                    loanProviderName = "My mocked API response",
                    approved = true,
                    accountId = 14121
                };

                await route.FulfillAsync(new()
                {
                    Json = mockResponse
                });

            });

            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");
                        
            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("10000", "1000", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Approved");
            await Expect(requestLoanPage.TextfieldLoanProvider).ToHaveTextAsync("My mocked API response");
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
