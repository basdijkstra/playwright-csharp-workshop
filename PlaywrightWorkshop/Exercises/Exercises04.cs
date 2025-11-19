using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Exercises.Pages.ParaBank;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises04 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            Page.SetDefaultTimeout(5000);
        }

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
            await Page.Locator("//input[@name='username']").FillAsync("john");
            await Page.Locator("//input[@name='password']").FillAsync("demo");
            await Page.Locator("//input[@value='Log In']").ClickAsync();

            /**
             * TODO: Replace this code with code that uses the AccountsOverviewPage object.
             * This class has already been defined. Call the SelectMenuItem() method to
             * navigate to the 'Request Loan' page.
             */
            await Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" }).ClickAsync();

            /**
             * TODO: Replace this code with code that uses the RequestLoanPage object.
             * You will need to define this class yourself.
             * 
             * Add a method SubmitLoanRequestFor() that takes the values needed to
             * submit a loan request and performs the required interactions. 
             * 
             * Also add a property TextfieldLoanApplicationResult that exposes the loan application
             * result, and use it in the assertion here in the test method.
             * 
             * If you don't know how to do this, the ParaBank page objects in the Examples folder
             * contain an example.
             */
            await Page.Locator("input[id=amount]").FillAsync("10000");
            await Page.Locator("input[id=downPayment]").FillAsync("1000");
            await Page.Locator("select[id=fromAccountId]").SelectOptionAsync("12345");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Apply Now" }).ClickAsync();

            await Expect(Page.Locator("td[id=loanStatus]")).ToHaveTextAsync("Denied");
        }

        [Test]
        public async Task LoanApplicationFor1000_WithDownPaymentOf500_IsApproved()
        {
            /**
             * TODO: Apply the changes to this test, too
             */

            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "INIT" }).ClickAsync();
            await Expect(Page.GetByText("Database Initialized")).ToBeVisibleAsync();

            await Page.Locator("input[name=username]").FillAsync("john");
            await Page.Locator("input[name=password]").FillAsync("demo");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" }).ClickAsync();

            await Page.Locator("input[id=amount]").FillAsync("1000");
            await Page.Locator("input[id=downPayment]").FillAsync("500");
            await Page.Locator("select[id=fromAccountId]").SelectOptionAsync("12345");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Apply Now" }).ClickAsync();

            await Expect(Page.Locator("td[id=loanStatus]")).ToHaveTextAsync("Approved");
        }
    }
}
