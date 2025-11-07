using Microsoft.Playwright;

namespace PlaywrightWorkshop.Exercises.Pages.SwagLabs
{
    public class ProductDetailPage
    {
        private readonly IPage page;

        // Define two ILocator objects here, one for the button that adds an item to the
        //  shopping cart, another to navigate to the shopping cart
        

        public ProductDetailPage(IPage page)
        {
            this.page = page;

            // Initialize the ILocator objects here. For the button navigating to the shopping
            //  cart, I recommend using "css=.shopping_cart_link" as the Locator.

        }

        // Add a method that clicks the button that adds the current item to the cart
        

        // Add a method that navigates to the shopping cart
        
    }
}
