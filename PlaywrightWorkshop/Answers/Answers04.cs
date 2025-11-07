using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Answers.Pages.ParaBank;

namespace PlaywrightWorkshop.Answers
{
    [TestFixture]
    public class Exercises04 : PageTest
    {
        [Test]
        public async Task LoanApplicationFor10000_WithDownPaymentOf1000_IsDenied()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page"}).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "INIT" }).ClickAsync();
            await Expect(Page.GetByText("Database Initialized")).ToBeVisibleAsync();

            /**
             * TODO: Replace this code with code that uses the LoginPage object.
             * This class has already been defined. First, call the Open() method,
             * then the LoginAs() method, passing in the credentials required to successfully log in.
             */
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            /**
             * TODO: Replace this code with code that uses the AccountsOverviewPage object.
             * This class has already been defined. Call the SelectMenuItem() method to
             * navigate to the 'Request Loan' page.
             */
            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");

            /**
             * TODO: Replace this code with code that uses the RequestLoanPage object.
             * You will need to define this class yourself.
             * 
             * Add a method SubmitLoanRequestFor() that takes the values needed to
             * submit a loan request and performs the required interactions. 
             * 
             * Also add a property TextfieldLoanApplicationResult that exposes the loan application
             * result, and use it in the assertion here in the test method.
             */
            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("10000", "1000", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Denied");
        }

        [Test]
        public async Task LoanApplicationFor1000_WithDownPaymentOf500_IsDenied()
        {
            /**
             * TODO: Apply the changes to this test, too
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
            await requestLoanPage.SubmitLoanRequestFor("1000", "500", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Approved");
        }
    }
}
