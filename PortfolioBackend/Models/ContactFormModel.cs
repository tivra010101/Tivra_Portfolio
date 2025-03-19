using System.Text.Json.Serialization;

namespace PortfolioBackend.Models
{
    public class ContactFormModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    [JsonSerializable(typeof(ContactFormModel))]
    public partial class ContactFormModelContext : JsonSerializerContext
    {
    }
}
