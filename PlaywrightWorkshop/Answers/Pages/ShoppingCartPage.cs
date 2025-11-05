using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages
{
    public class ShoppingCartPage
    {
        public readonly IPage page;
        public ILocator CheckoutButton { get; init; }

        public ShoppingCartPage(IPage page)
        {
            this.page = page;
            this.CheckoutButton = this.page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });
        }
    }
}
