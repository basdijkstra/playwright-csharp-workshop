using Microsoft.Playwright.NUnit;
using PlaywrightWorkshop.Exercises.Pages;

namespace PlaywrightWorkshop.Exercises
{
    [TestFixture]
    public class Exercises02 : PageTest
    {
        [SetUp]
        public void SetUp()
        {
            Page.SetDefaultTimeout(5000);
        }

        [Test]
        public async Task UserCanOrderASauceLabsBackpack()
        {
            // Create a new LoginPage instance (the class and methods already exist)
            //  then first call Open() on it, then LoginAs(), passing the credentials as arguments
            

            // Create a new ProductsOverviewPage instance (the class already exists). Then,
            //  in the page object class, create a method to select the product with the name "Sauce Labs Backpack"
            //  and then call it here
            

            // Create a new ProductDetailPage instance (the class already exists). Then,
            //  in the page object class, create one or more methods to add the item to the
            //  shopping cart and then open the shopping cart.
            

            // Create a new ShoppingCartPage class with a constructor and a public ILocator field for the checkout button.
            //  See the RequestLoanPage in the examples for an example on how to do that. Add an assertion that verifies that
            //  the button is visible - this will be our verification that the test has completed.
            
        }
    }
}
