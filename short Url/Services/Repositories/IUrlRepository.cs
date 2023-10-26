using short_Url.Models;

namespace short_Url.Services.Repositories
{
    public interface IUrlRepository
    {
        void Add(Url url);

        Url GetById(Guid id);

        void Delete(Url url);

        List<Url> GetAll();

        bool CheckIfShortnerExists(string code);

        Url GetUrlByShortener(string shortener);

        Task Save();
    }
}
