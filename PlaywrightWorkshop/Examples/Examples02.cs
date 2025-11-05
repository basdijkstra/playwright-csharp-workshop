using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Examples.Pages;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class Examples02 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            Page.SetDefaultTimeout(5000);
        }

        [Test]
        public async Task UserCanSubmitALoanApplicationAndSeeTheResult()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.Locator("xpath=//input[@name='username']").FillAsync("john");
            await Page.Locator("xpath=//input[@name='password']").FillAsync("demo");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" }).ClickAsync();

            await Page.Locator("xpath=//input[@id='amount']").FillAsync("1000");
            await Page.Locator("xpath=//input[@id='downPayment']").FillAsync("100");
            await Page.Locator("xpath=//select[@id='fromAccountId']").SelectOptionAsync("12345");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Apply Now" }).ClickAsync();

            await Expect(Page.Locator("xpath=//td[@id='loanStatus']")).ToHaveTextAsync("Approved");
        }

        [Test]
        public async Task UserCanSubmitALoanApplicationAndSeeTheResult_UsingPageObjects()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");

            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("1000", "100", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Approved");
        }
    }
}
