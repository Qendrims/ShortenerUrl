using short_Url.Data;
using short_Url.Models;

namespace short_Url.Services.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UrlRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Add(Url url) => _applicationDbContext.Urls.Add(url);

        public bool CheckIfShortnerExists(string code)
        {
            var url = _applicationDbContext.Urls.Where(x=>x.ShortUrl == code).FirstOrDefault();

            if (url == null) return true;

            return false;
        }

        public void Delete(Url url) => _applicationDbContext.Urls.Remove(url);


        public List<Url> GetAll() => _applicationDbContext.Urls.ToList();
        
        

        public Url GetById(Guid id) => _applicationDbContext.Urls.FirstOrDefault(x => x.Id == id);

        public Url GetUrlByShortener(string shortener) => _applicationDbContext.Urls.FirstOrDefault(x => x.ShortUrl == shortener);

        public Task Save() =>  _applicationDbContext.SaveChangesAsync();
    }
}
