using System.Text.Json.Serialization;

namespace PlaywrightWorkshop.Answers.Models
{
    public class LoanApplicationResponse
    {
        [JsonPropertyName("loanProviderName")]
        public string LoanProviderName { get; set; } = string.Empty;

        [JsonPropertyName("approved")]
        public bool Approved { get; set; }

        [JsonPropertyName("accountId")]
        public int? AccountId { get; set; }
    }
}
