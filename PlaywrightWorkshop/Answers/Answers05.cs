using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Answers
{
    [TestFixture]
    public class Answers05 : PageTest
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

        [TestCase("10000", "1000", "12345", "Denied", TestName = "Loan application for 10000 with 1000 down payment is denied")]
        [TestCase("1000", "500", "12345", "Approved", TestName = "Loan application for 1000 with 500 down payment is approved")]
        public async Task SubmitLoanApplication_CheckResult_ShouldMatchExpected
            (string amount, string downPayment, string fromAccountId, string expectedResult)
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
            await requestLoanPage.SubmitLoanRequestFor(amount, downPayment, fromAccountId);

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync(expectedResult);
        }
    }
}
