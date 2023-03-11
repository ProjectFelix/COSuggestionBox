namespace COSuggestionBox.Models
{
    public class SuggestionReturnModel
    {
        public long SuggestionId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
    }
}
