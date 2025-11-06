using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Examples.Pages;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class ExampleTraceRecording : PageTest
    {
        [SetUp]
        public async Task SetupTrace()
        {
            Page.SetDefaultTimeout(5000);

            await Context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [Test]
        public async Task APassingTest()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");

            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("1000", "100", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Approved");
        }

        [Test]
        public async Task AFailingTest()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.Open();
            await loginPage.LoginAs("john", "demo");

            await new AccountsOverviewPage(Page).SelectMenuItem("Request Loan");

            var requestLoanPage = new RequestLoanPage(Page);
            await requestLoanPage.SubmitLoanRequestFor("1000", "100", "12345");

            await Expect(requestLoanPage.TextfieldLoanApplicationResult).ToHaveTextAsync("Error");
        }

        [TearDown]
        public async Task TeardownTrace()
        {
            var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
            || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

            await Context.Tracing.StopAsync(new()
            {
                Path = failed ? Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                ) : null,
            });
        }
    }
}
