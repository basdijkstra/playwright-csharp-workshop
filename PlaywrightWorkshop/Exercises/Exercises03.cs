using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises03 : PageTest
    {
        [Test]
        public async Task LoanApplicationFor10000_WithDownPaymentOf1000_IsDenied()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page"}).ClickAsync();
            await Page.Locator("xpath=//button[@value='INIT']").ClickAsync();
            await Expect(Page.Locator("//b[text()='Database Initialized']")).ToBeVisibleAsync();

            await Page.Locator("//input[@name='username']").FillAsync("john");
            await Page.Locator("//input[@name='password']").FillAsync("demo");
            await Page.Locator("//input[@value='Log In']").ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" }).ClickAsync();

            await Page.Locator("//input[@id='amount']").FillAsync("10000");
            await Page.Locator("//input[@id='downPayment']").FillAsync("1000");
            await Page.Locator("//select[@id='fromAccountId']").SelectOptionAsync("12345");
            await Page.Locator("//input[@value='Apply Now']").ClickAsync();

            await Expect(Page.Locator("//td[@id='loanStatus']")).ToHaveTextAsync("Denied");
        }

        [Test]
        public async Task LoanApplicationFor1000_WithDownPaymentOf500_IsApproved()
        {
            await Page.GotoAsync("https://parabank.parasoft.com");

            await Page.GetByRole(AriaRole.Link, new() { Name = "Admin Page" }).ClickAsync();
            await Page.Locator("xpath=//button[@value='INIT']").ClickAsync();
            await Expect(Page.Locator("//b[text()='Database Initialized']")).ToBeVisibleAsync();

            await Page.Locator("//input[@name='username']").FillAsync("john");
            await Page.Locator("//input[@name='password']").FillAsync("demo");
            await Page.Locator("//input[@value='Log In']").ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" }).ClickAsync();

            await Page.Locator("//input[@id='amount']").FillAsync("1000");
            await Page.Locator("//input[@id='downPayment']").FillAsync("500");
            await Page.Locator("//select[@id='fromAccountId']").SelectOptionAsync("12345");
            await Page.Locator("//input[@value='Apply Now']").ClickAsync();

            await Expect(Page.Locator("//td[@id='loanStatus']")).ToHaveTextAsync("Approved");
        }
    }
}
