using FaturaTakip.Models;

namespace FaturaTakipAPI.Services
{
    public interface ISirketService
    {
        Sirketler GetSirketById(int id);
        Sirketler GetSirketByEmail(string email);
        void CreateSirket(Sirketler sirket);
        void UpdateSirket(Sirketler sirket);
    }
    public class SirketService : ISirketService
    {
        // Burada DbContext veya başka bir veritabanı erişimi için gerekli kodlar olur.
        private readonly FaturaTakipDbContext _dbContext;
        public SirketService(FaturaTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateSirket(Sirketler sirket)
        {
            throw new NotImplementedException();
        }

        public Sirketler GetSirketByEmail(string email)
        {
            var sirket = _dbContext.Sirketler.FirstOrDefault(m => m.Eposta == email);
            return sirket;
        }

        public Sirketler GetSirketById(int id)
        {
            var sirket = _dbContext.Sirketler.FirstOrDefault(m => m.SirketID == id);
            return sirket;
        }

        public void UpdateSirket(Sirketler sirket)
        {
            throw new NotImplementedException();
        }
    }
}
