using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class Examples06 : PageTest
    {
        private IAPIRequestContext apiContext;

        [SetUp]
        public async Task SetUpApiContext()
        {
            var headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
            };

            this.apiContext = await this.Playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "https://jsonplaceholder.typicode.com",
                ExtraHTTPHeaders = headers
            });
        }

        [Test]
        public async Task UserDataCanBeRetrieved()
        {
            var response = await this.apiContext.GetAsync("/users/1");
            
            // Playwright-native assertion
            await Expect(response).ToBeOKAsync();

            // Vanilla NUnit assertion
            Assert.That(response.Status, Is.EqualTo(200));

        }

        [TearDown]
        public async Task CleanUp()
        {
            await this.apiContext.DisposeAsync();
        }
    }
}
