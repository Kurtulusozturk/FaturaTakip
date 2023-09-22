using FaturaTakip.Models;
using Microsoft.EntityFrameworkCore;

namespace FaturaTakipAPI.Services
{
    public interface IMusteriService
    {
        IEnumerable<Musteriler> GetAllMusteriler();
        Musteriler GetMusteriById(int id);
        void CreateMusteri(Musteriler musteri);
        void UpdateMusteri(Musteriler musteri);
        void DeleteMusteri(int id);
    }
    public class MusteriService : IMusteriService
    {
        // Burada DbContext veya başka bir veritabanı erişimi için gerekli kodlar olur.
        private readonly FaturaTakipDbContext _dbContext;
        public MusteriService(FaturaTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Done
        public IEnumerable<Musteriler> GetAllMusteriler()
        {
            return _dbContext.Musteriler.ToList();
        }
        public void CreateMusteri(Musteriler musteri)
        {
            _dbContext.Musteriler.Add(musteri);
            _dbContext.SaveChanges();          
        }
        //Done
        public void DeleteMusteri(int id)
        {
            var musteri = _dbContext.Musteriler.FirstOrDefault(m => m.MusteriID == id);

            if (musteri != null)
            {
                musteri.Durum = false;
                _dbContext.SaveChanges();
            }
        }
        //Done
        public Musteriler GetMusteriById(int id)
        {
            var musteri = _dbContext.Musteriler.FirstOrDefault(m => m.MusteriID == id);
            return musteri;
        }

        public void UpdateMusteri(Musteriler musteri)
        {
            var existingMusteri = _dbContext.Musteriler.FirstOrDefault(m => m.MusteriID == musteri.MusteriID);
            if (existingMusteri != null)
            {
                existingMusteri.Ad = musteri.Ad;
                existingMusteri.Soyad = musteri.Soyad;
                existingMusteri.Adres = musteri.Adres;
                existingMusteri.Eposta = musteri.Eposta;
                existingMusteri.TelefonNo = musteri.TelefonNo;
                _dbContext.SaveChanges();
            }
        }
    }
}
