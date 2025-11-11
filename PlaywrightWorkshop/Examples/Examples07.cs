using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Examples.Models;

namespace PlaywrightWorkshop.Examples
{
    [TestFixture]
    public class Examples07 : PageTest
    {
        private IAPIRequestContext apiContext;

        [SetUp]
        public async Task SetUp()
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
        public async Task GetUserData_CheckName_ShouldBeLeanneGraham()
        {
            var getUserOneResponse = await apiContext.GetAsync("/users/1");

            Assert.That(getUserOneResponse.Status, Is.EqualTo(200));

            // Using deserialization into a JsonElement
            var bodyAsJsonElement = await getUserOneResponse.JsonAsync();
            Assert.That(bodyAsJsonElement.Value.GetProperty("name").GetString(), Is.EqualTo("Leanne Graham"));

            // Using deserialization into a User object
            var bodyAsTypedObject = await getUserOneResponse.JsonAsync<User>();
            Assert.That(bodyAsTypedObject.Name, Is.EqualTo("Leanne Graham"));
        }

        [Test]
        public async Task PostNewPost_CheckStatusCode_ShouldBeHttpCreated()
        {
            var post = new Dictionary<string, object>()
            {
                { "title", "my new blog post" },
                { "body", "awesome content here" },
                { "userId", 1 }
            };

            var postPostResponse = await this.apiContext.PostAsync("/posts", new()
            {
                DataObject = post
            });

            Assert.That(postPostResponse.Status, Is.EqualTo(201));
        }

        [TearDown]
        public async Task CleanUp()
        {
            await this.apiContext.DisposeAsync();
        }
    }
}
