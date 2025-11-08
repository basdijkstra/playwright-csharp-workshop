using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Answers
{
    [TestFixture]
    public class Answers03 : PageTest
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

            await Page.Locator("input[name=username]").FillAsync("john");
            await Page.Locator("input[name=password]").FillAsync("demo");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" }).ClickAsync();

            await Page.Locator("input[id=amount]").FillAsync("10000");
            await Page.Locator("input[id=downPayment]").FillAsync("1000");
            await Page.Locator("select[id=fromAccountId]").SelectOptionAsync("12345");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Apply Now" }).ClickAsync();

            await Expect(Page.Locator("td[id=loanStatus]")).ToHaveTextAsync("Denied");
        }

        [Test]
        public async Task LoanApplicationFor1000_WithDownPaymentOf500_IsDenied()
        {
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
