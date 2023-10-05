using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
using FaturaTakipAPI.Models.Response;
using FaturaTakipAPI.Models.Response.Musteri;
using Microsoft.EntityFrameworkCore;

namespace FaturaTakipAPI.Services
{
    public interface IMusteriService
    {
        List<MusterilerGetModel> GetMusterilerBySirketId(int id);
        MusterilerGetModel GetMusteriById(int id);
        string CreateMusteri(MusterilerCreateAndUpdateModel musteri);
        string UpdateMusteri(int id, MusterilerCreateAndUpdateModel musteri);
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

        public string CreateMusteri(MusterilerCreateAndUpdateModel musteri)
        {
            var sirket = _dbContext.Sirketler?.FirstOrDefault(s => s.SirketID == musteri.SirketID);

            if (sirket != null)
            {
                var newMusteri = new Musteriler
                {
                    Ad = musteri.Ad,
                    Soyad = musteri.Soyad,
                    Adres = musteri.Adres,
                    Eposta = musteri.Eposta,
                    TelefonNo = musteri.TelefonNo,
                    Fatura = null,
                    Sirket = sirket,
                    Durum = musteri.Durum,
                };

                _dbContext.Musteriler?.Add(newMusteri);
                _dbContext.SaveChanges();
                return ("Yeni kayıt oluşturuldu");
            }
            return ("Fatura alanı dolu olmalı");
        }

        public void DeleteMusteri(int id)
        {
            var musteri = _dbContext.Musteriler.Include(f => f.Fatura).Include(f => f.Sirket).FirstOrDefault(m => m.MusteriID == id);
            if (musteri != null)
            {
                musteri.Durum = false;
                _dbContext.SaveChanges();
            }
        }

        public MusterilerGetModel GetMusteriById(int id)
        {
            var musteri = _dbContext.Musteriler.Include(f => f.Fatura).Include(f => f.Sirket).FirstOrDefault(m => m.MusteriID == id);
            if (musteri != null)
            {
                var musteriIdAndSiparisNoList = new List<MusteriIdSiparisNoModel>();
                foreach (var item in musteri.Fatura.ToList())
                {
                    var musteriIdAndSiparisNo = new MusteriIdSiparisNoModel();
                    musteriIdAndSiparisNo.FaturaID = item.FaturaID;
                    musteriIdAndSiparisNo.SiparisNo = item.SiparisNo;
                    musteriIdAndSiparisNoList.Add(musteriIdAndSiparisNo);
                }

                var showMusteri = new MusterilerGetModel
                {
                    Ad = musteri.Ad,
                    Soyad = musteri.Soyad,
                    Adres = musteri.Adres,
                    Eposta = musteri.Eposta,
                    TelefonNo = musteri.TelefonNo,
                    musteriIdAndSiparisNo = musteriIdAndSiparisNoList,
                    Durum = musteri.Durum,
                };
                return showMusteri;
            }
            return null;
        }

        public List<MusterilerGetModel> GetMusterilerBySirketId(int id)
        {
            var musterilerList = new List<MusterilerGetModel>();
            foreach (var item in _dbContext.Musteriler.Include(f => f.Fatura).Include(f => f.Sirket).ToList())
            {
                if (item.Sirket?.SirketID == id)
                {
                    var musteriIdAndSiparisNoList = new List<MusteriIdSiparisNoModel>();
                    foreach (var i in item.Fatura.ToList())
                    {
                        var musteriIdAndSiparisNo = new MusteriIdSiparisNoModel();
                        musteriIdAndSiparisNo.FaturaID = i.FaturaID;
                        musteriIdAndSiparisNo.SiparisNo = i.SiparisNo;
                        musteriIdAndSiparisNoList.Add(musteriIdAndSiparisNo);
                    }
                    var showMusteri = new MusterilerGetModel
                    {
                        MusteriID = item.MusteriID,
                        Ad = item.Ad,
                        Soyad = item.Soyad,
                        Adres = item.Adres,
                        Eposta = item.Eposta,
                        TelefonNo = item.TelefonNo,
                        musteriIdAndSiparisNo = musteriIdAndSiparisNoList,
                        Durum = item.Durum
                    };
                    musterilerList.Add(showMusteri);
                }
            }
            return musterilerList;
        }

        public string UpdateMusteri(int id,MusterilerCreateAndUpdateModel musteri)
        {
            var dbMusteri = _dbContext.Musteriler.Include(f => f.Fatura).Include(f => f.Sirket).FirstOrDefault(m => m.MusteriID == id);
            if (dbMusteri != null)
            {
                var sirket = dbMusteri.Sirket;
                var fatura = dbMusteri.Fatura;
                if (sirket != null)
                {
                    dbMusteri.Ad = musteri.Ad; 
                    dbMusteri.Soyad = musteri.Soyad;
                    dbMusteri.Adres = musteri.Adres;
                    dbMusteri.Eposta = musteri.Eposta;
                    dbMusteri.TelefonNo = musteri.TelefonNo;
                    dbMusteri.Fatura = fatura;
                    dbMusteri.Sirket = sirket;
                    dbMusteri.Durum = musteri.Durum;

                    _dbContext.SaveChanges();
                    return ("Musteri güncellendi");
                }
                return ("Sirket Bulunamadı");
            }
            return ("Musteri Bulunamadı");
        }
    }
}
