namespace short_Url.Models
{
    public class Url
    {
        public Guid Id { get; set; }

        public string LongUrl { get; set; } = string.Empty;

        public string ShortUrl { get; set; } = string.Empty;

        public DateTime? ExpiredDate { get; set; }
    }
}
