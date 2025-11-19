using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Examples.Pages;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class Examples05 : PageTest
    {
        [TestCase("john", "demo", TestName = "User 'john' can log in")]
        [TestCase("parasoft", "demo", TestName = "User 'parasoft' can log in")]
        public async Task UserCanLogIn(string username, string password)
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs(username, password);

            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" })).ToBeVisibleAsync();
        }

        [Test]
        public async Task JohnCanLogIn()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" })).ToBeVisibleAsync();
        }

        [Test]
        public async Task ParasoftCanLogIn()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("parasoft", "demo");

            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" })).ToBeVisibleAsync();
        }

        [TestCaseSource(nameof(Credentials))]
        public async Task UserCanLogInUsingTestCaseSource(string username, string password)
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs(username, password);

            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Request Loan" })).ToBeVisibleAsync();
        }

        private static IEnumerable<TestCaseData> Credentials()
        {
            yield return new TestCaseData("john", "demo").SetName("User 'john' can log in - using TestCaseSource");
            yield return new TestCaseData("parasoft", "demo").SetName("User 'parasoft' can log in - using TestCaseSource");
        }
    }
}
