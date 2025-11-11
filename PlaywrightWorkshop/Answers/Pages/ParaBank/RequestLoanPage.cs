using Microsoft.Playwright;

namespace PlaywrightWorkshop.Answers.Pages.ParaBank
{
    public class RequestLoanPage
    {
        private readonly IPage page;

        private readonly ILocator textfieldAmount;
        private readonly ILocator textfieldDownPayment;
        private readonly ILocator dropdownFromAccountId;
        private readonly ILocator buttonSubmitApplication;

        public ILocator TextfieldLoanApplicationResult { get; init; }
        public ILocator TextfieldLoanProvider {  get; init; }

        public RequestLoanPage(IPage page)
        {
            this.page = page;
            this.textfieldAmount = this.page.Locator("xpath=//input[@id='amount']");
            this.textfieldDownPayment = this.page.Locator("xpath=//input[@id='downPayment']");
            this.dropdownFromAccountId = this.page.Locator("xpath=//select[@id='fromAccountId']");
            this.buttonSubmitApplication = this.page.GetByRole(AriaRole.Button, new() { Name = "Apply Now" });

            this.TextfieldLoanApplicationResult = this.page.Locator("xpath=//td[@id='loanStatus']");
            this.TextfieldLoanProvider = this.page.Locator("xpath=//td[@id='loanProviderName']");
        }

        public async Task SubmitLoanRequestFor(string amount, string downPayment, string fromAccountId)
        {
            await this.textfieldAmount.FillAsync(amount);
            await this.textfieldDownPayment.FillAsync(downPayment);
            await this.dropdownFromAccountId.SelectOptionAsync(fromAccountId);
            await this.buttonSubmitApplication.ClickAsync();
        }
    }
}
