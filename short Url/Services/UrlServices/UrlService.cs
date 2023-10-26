using short_Url.DTO;
using short_Url.Models;
using short_Url.Services.Repositories;

namespace short_Url.Services.UrlServices
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;
        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public List<UrlDto> GetAllShortedUrl(IHttpContextAccessor httpContextAccessor)
        {
            var urlDtoList = new List<UrlDto>();
            var url = _urlRepository.GetAll();
            if (url != null)
            {
                foreach (var item in url)
                {
                    var urlDto = new UrlDto
                    {
                        ShortedUrl = httpContextAccessor.HttpContext.Request.Scheme+"://" + httpContextAccessor.HttpContext.Request.Host + "/shorturl/" + item.ShortUrl,
                        LongUrl = item.LongUrl,
                    };
                    urlDtoList.Add(urlDto);
                }
            }
            return urlDtoList;
        }



        public bool CreateShortenerUrl(string longUrl, int expiredDate = 0)
        {
            if (expiredDate > 0)
            {
                var url = new Url
                {
                    Id = Guid.NewGuid(),
                    LongUrl = longUrl,
                    ShortUrl = GenerateUniqueShortener(),
                    ExpiredDate = DateTime.Now.AddMinutes(expiredDate),
                };
                _urlRepository.Add(url);
                _urlRepository.Save();
                return true;

            }

            var urlWithoutExpiredDate = new Url
            {
                Id = Guid.NewGuid(),
                LongUrl = longUrl,
                ShortUrl = GenerateUniqueShortener()
            };
            _urlRepository.Add(urlWithoutExpiredDate);
            _urlRepository.Save();
            return true;
        }


        public bool DeleteUrl(string shortenerUrl)
        {
            if (shortenerUrl != null)
            {
                var url = _urlRepository.GetUrlByShortener(shortenerUrl);
                _urlRepository.Delete(url);

                _urlRepository.Save();
                return true;
            }
            return false;
        }



        public string RedirectToLongUrl(string shorterUrl)
        {
            var url = _urlRepository.GetUrlByShortener(shorterUrl);


            return url.LongUrl;
        }

        private string GenerateUniqueShortener()
        {
            const int numberOfChars = 7;
            const string shortnerChars = "ABCDEFGHIJKLMNOPKRSTUVWXYZabcdefghijklmnopkrstuvwxyz01234567890";

            var random = new Random();

            var codeChars = new char[numberOfChars];


            for (var i = 0; i < numberOfChars; i++)
            {
                var randomIndex = random.Next(shortnerChars.Length - 1);

                codeChars[i] = shortnerChars[randomIndex];
            }

            var code = new string(codeChars);

            if (!_urlRepository.CheckIfShortnerExists(code))
            {
                return GenerateUniqueShortener();
            }
            return code;


        }
    }
}
