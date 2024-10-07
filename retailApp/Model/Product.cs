using System.Text.Json.Serialization;

namespace retailApp.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool IsQualified { get; set; }

        // Holds the disqualification reason in the response only when IsQualified is false
        public string? DisqualificationReason { get; set; }

        // This property will hold the entity from the database (ignored in JSON)
        [JsonIgnore]
        public DisqualificationReason? DisqualificationReasonEntity { get; set; }

        // Method to return the disqualification reason if the product is disqualified
        public string? GetDisqualificationReason()
        {
            return !IsQualified ? DisqualificationReasonEntity?.Reason : null;
        }
    }
}

