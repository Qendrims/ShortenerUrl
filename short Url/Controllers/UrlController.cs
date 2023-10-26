
using Microsoft.AspNetCore.Mvc;
using short_Url.DTO;
using short_Url.Services.UrlServices;

namespace short_Url.Controllers
{
    public class UrlController : Controller
    {
        IHttpContextAccessor _contextAccessor;
        private UrlService _urlService;
        public UrlController(UrlService urlService, IHttpContextAccessor httpContextAccessor)
        {
            _urlService = urlService;
            _contextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult ShowUrl()
        {
            var urlDto = _urlService.GetAllShortedUrl(_contextAccessor);
            return View(urlDto);
        }
        [HttpPost]
        public bool CreateShortenerUrl(string longUrl, int expriedDate = 0)
        {
            var isCreated = _urlService.CreateShortenerUrl(longUrl, expriedDate);

            return isCreated;
        }

        [HttpDelete]
        public bool DeleteUrl(string shorterUrl)
        {
            var isDeleted =_urlService.DeleteUrl(shorterUrl.Split('/')[4]);

            if(isDeleted) return true;

            return false;

        }
        [Route("shorturl/{shortenerUrl}")]
        public IActionResult RedirectToUrl(string shortenerUrl)
        {
            var longUrl = _urlService.RedirectToLongUrl(shortenerUrl);
            return Redirect(longUrl);
        }
    }
}
