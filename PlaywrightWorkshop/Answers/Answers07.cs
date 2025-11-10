using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Models;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Answers
{
    [TestFixture]
    public class Answers07 : PageTest
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

        [TestCase("10000", "1000", "12345", false, TestName = "Loan application for 10000 with 1000 down payment is denied")]
        [TestCase("1000", "500", "12345", true, TestName = "Loan application for 1000 with 500 down payment is approved")]
        public async Task SubmitLoanApplication_CheckResult_ShouldMatchExpected
            (string amount, string downPayment, string fromAccountId, bool approved)
        {
            /**
             * TODO: Replace the UI automation code with a POST call to /parabank/services/bank
             * Add the following query parameters:
             *   * customerId = 12212
             *   * amount = amount
             *   * downPayment = downPayment
             *   * fromAccountId = fromAccountId
             * 
             * Check that the response status code is equal to HTTP 200
             * 
             * Check that the value of the 'approved' field in the response body equals the value
             *   of the approved parameter
             *   
             * You can choose whether to do this by serializing the response into an object of
             *   type LoanApplicationResponse (which is defined for you already), or by
             *   deserializing it into an object of type JsonElement and using .Value.GetProperty()
             */

            var loanApplicationResponse = await apiContext.PostAsync("/parabank/services/bank/requestLoan", new()
            {
                Params = new Dictionary<string, object>
                {
                    { "customerId", 12212 },
                    { "amount", amount },
                    { "downPayment", downPayment },
                    { "fromAccountId", fromAccountId }
                }
            });

            Assert.That(loanApplicationResponse.Status, Is.EqualTo(200));

            // Using deserialization into a JsonElement
            var bodyAsJsonElement = await loanApplicationResponse.JsonAsync();
            Assert.That(bodyAsJsonElement.Value.GetProperty("approved").GetBoolean(), Is.EqualTo(approved));

            // Using deserialization into a LoanApplicationResponse object
            var bodyAsTypedObject = await loanApplicationResponse.JsonAsync<LoanApplicationResponse>();
            Assert.That(bodyAsTypedObject.Approved, Is.EqualTo(approved));
        }

        [TearDown]
        public async Task CleanUp()
        {
            await this.apiContext.DisposeAsync();
        }
    }
}
