using Microsoft.Playwright;

namespace PlaywrightWorkshop.Examples.Components
{
    public class MenuComponent
    {
        private readonly IPage page;

        public MenuComponent(IPage page)
        {
            this.page = page;
        }

        public async Task SelectMenuItem(string menuItem)
        {
            await this.page.GetByRole(AriaRole.Link, new() { Name = menuItem }).ClickAsync();
        }
    }
}
