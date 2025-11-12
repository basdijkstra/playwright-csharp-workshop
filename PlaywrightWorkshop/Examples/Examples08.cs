using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class Examples08 : PageTest
    {
        [SetUp]
        public async Task SetUp()
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
        public async Task GetFruits()
        {
            await Page.RouteAsync("*/**/api/v1/fruits", async route =>
            {
                var mockResponse = new[]
                {
                    new
                    {
                        name = "Some weird kind of fruit",
                        id = 21
                    }
                };
                
                await route.FulfillAsync(new()
                {
                    Json = mockResponse
                });

            });

            // Go to the page
            await Page.GotoAsync("https://demo.playwright.dev/api-mocking");

            // Assert that the Strawberry fruit is visible
            await Expect(Page.GetByText("Some weird kind of fruit")).ToBeVisibleAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                )
            });
        }
    }
}
