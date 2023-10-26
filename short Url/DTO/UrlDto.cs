using short_Url.Models;

namespace short_Url.DTO
{
    public class UrlDto
    {
        public string LongUrl { get; set; } = string.Empty;

        public string ShortedUrl { get; set; } = string.Empty;

        public int? ExpireDate { get; set; }
    }

}
