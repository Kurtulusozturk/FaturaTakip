using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;

namespace FaturaTakipAPI.Services
{
    public interface IFaturaService
    {
        IEnumerable<Faturalar> GetAllFaturalar();
        Faturalar GetFaturaById(int id);
        void CreateFatura(FaturalarCreatAndUpdateModel fatura);
        void UpdateFatura(int id, FaturalarCreatAndUpdateModel fatura);
        void DeleteFatura(int id);
    }
    public class FaturaService : IFaturaService
    {
        // Burada DbContext veya başka bir veritabanı erişimi için gerekli kodlar olur.
        private readonly FaturaTakipDbContext _dbContext;
        public FaturaService(FaturaTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateFatura(FaturalarCreatAndUpdateModel fatura)
        {
            var sirket = _dbContext.Sirketler.FirstOrDefault(s => s.SirketID == fatura.SirketID);
            var musteri = _dbContext.Musteriler.FirstOrDefault(m => m.MusteriID == fatura.MusteriID);

            if (sirket != null || musteri != null)
            {
                var newFatura = new Faturalar
                {
                    FaturaNo = fatura.FaturaNo,
                    FaturaTarihi = fatura.FaturaTarihi,
                    SiparisNo = fatura.SiparisNo,
                    SiparisTarihi = fatura.SiparisTarihi,
                    Urun = fatura.Urun,
                    Miktar = fatura.Miktar,
                    BirimFiyat = fatura.BirimFiyat,
                    KDV = fatura.KDV,
                    KDVOrani = fatura.KDVOrani,
                    OdenecekTutar = fatura.OdenecekTutar,
                    Sirket = sirket,
                    Musteri = musteri,
                    Durum = fatura.Durum,
                };

                _dbContext.Faturalar.Add(newFatura);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteFatura(int id)
        {
            var fatura = _dbContext.Faturalar.FirstOrDefault(m => m.FaturaID == id);

            if (fatura != null)
            {
                fatura.Durum = false;
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Faturalar> GetAllFaturalar()
        {
            return _dbContext.Faturalar.ToList();
        }

        public Faturalar GetFaturaById(int id)
        {
            var fatura = _dbContext.Faturalar.FirstOrDefault(m => m.FaturaID == id);
            return fatura;
        }

        public void UpdateFatura(int id, FaturalarCreatAndUpdateModel fatura)
        {
            var dbFatura = _dbContext.Faturalar.FirstOrDefault(m => m.FaturaID == id);
            if (dbFatura != null)
            {
                var sirket = dbFatura.Sirket;
                var musteri = _dbContext.Musteriler.FirstOrDefault(m => m.MusteriID == fatura.MusteriID);

                if (sirket != null || musteri != null)
                {

                    dbFatura.FaturaNo = fatura.FaturaNo;
                    dbFatura.FaturaTarihi = fatura.FaturaTarihi;
                    dbFatura.SiparisNo = fatura.SiparisNo;
                    dbFatura.SiparisTarihi = fatura.SiparisTarihi;
                    dbFatura.Urun = fatura.Urun;
                    dbFatura.Miktar = fatura.Miktar;
                    dbFatura.BirimFiyat = fatura.BirimFiyat;
                    dbFatura.KDV = fatura.KDV;
                    dbFatura.KDVOrani = fatura.KDVOrani;
                    dbFatura.Sirket = sirket;
                    dbFatura.Musteri = musteri;
                    dbFatura.Durum = fatura.Durum;
                    _dbContext.SaveChanges();
                }
            }


        }
    }
}
