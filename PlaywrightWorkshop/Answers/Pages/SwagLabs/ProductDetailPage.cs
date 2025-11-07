using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages.SwagLabs
{
    public class ProductDetailPage
    {
        private readonly IPage page;

        // Define two ILocator objects here, one for the button that adds an item to the
        //  shopping cart, another to navigate to the shopping cart
        private readonly ILocator buttonAddToCart;
        private readonly ILocator buttonGotoShoppingCart;

        public ProductDetailPage(IPage page)
        {
            this.page = page;

            // Initialize the ILocator objects here. For the button navigating to the shopping
            //  cart, I recommend using "css=.shopping_cart_link" as the Locator.
            buttonAddToCart = this.page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" });
            buttonGotoShoppingCart = this.page.Locator("css=.shopping_cart_link");
        }

        // Add a method that clicks the button that adds the current item to the cart
        public async Task AddItemToCart()
        {
            await buttonAddToCart.ClickAsync();
        }

        // Add a method that navigates to the shopping cart
        public async Task GotoShoppingCart()
        {
            await buttonGotoShoppingCart.ClickAsync();
        }
    }
}
