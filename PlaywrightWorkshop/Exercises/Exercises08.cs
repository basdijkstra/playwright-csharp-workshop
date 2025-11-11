using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises08 : PageTest
    {
        private IAPIRequestContext apiContext;

        [SetUp]
        public async Task SetUp()
        {
            Page.SetDefaultTimeout(5000);

            var headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
            };

            this.apiContext = await this.Playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "https://parabank.parasoft.com",
                ExtraHTTPHeaders = headers
            });

            var response = await this.apiContext.PostAsync("/parabank/services/bank/initializeDB");
            Assert.That(response.Status, Is.EqualTo(204));
        }

        [Test]
        public async Task SubmitLoanApplication_CheckResult_ShouldMatchExpected()
        {
            /** 
             * TODO: define a mock response for the API call that the browser makes to
             *   '* / ** / requestLoan?customerId=12212&amount=10000&downPayment=1000&fromAccountId=12345'
             *   (remove the spaces).
             *   
             * The mock JSON response should have these fields and values:
             *   responseDate = 1758177294806
             *   loanProviderName = "My mocked API response"
             *   approved = true
             *   accountId = 14121
             */

            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");
                        
            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("10000", "1000", "12345");

            /**
             * TODO: add Expect verifications that check that the loan application result is "Approved"
             *   and that the loan provider name is "My mocked API response". Both text fields are 
             *   defined in and exposed by the RequestLoanPage object already, so you don't need to
             *   make changes to that object yourself
             */

        }

        [TearDown]
        public async Task CleanUp()
        {
            await this.apiContext.DisposeAsync();
        }
    }
}
