﻿using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
using FaturaTakipAPI.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace FaturaTakipAPI.Services
{
    public interface IFaturaService
    {
        IEnumerable<FaturalarGetModel> GetAllFaturalar();
        FaturalarGetModel GetFaturaById(int id);
        string CreateFatura(FaturalarCreatAndUpdateModel fatura);
        string UpdateFatura(int id, FaturalarCreatAndUpdateModel fatura);
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
        public string CreateFatura(FaturalarCreatAndUpdateModel fatura)
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
                return ("Yeni kayıt oluşturuldu");
            }
            return ("Sirket ve Musteri alanları dolu olmalı");
        }

        public void DeleteFatura(int id)
        {
            var fatura = _dbContext.Faturalar.Include(f => f.Musteri).Include(f => f.Sirket).FirstOrDefault(m => m.FaturaID == id);

            if (fatura != null)
            {
                fatura.Durum = false;
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<FaturalarGetModel> GetAllFaturalar()
        {
            var faturalarList = new List<FaturalarGetModel>();
            foreach (var item in _dbContext.Faturalar.ToList())
            {
                var fatura = _dbContext.Faturalar.Include(f => f.Musteri).Include(f => f.Sirket).FirstOrDefault(m => m.FaturaID == item.FaturaID);
                var showFatura = new FaturalarGetModel
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
                    SirketID = fatura.Sirket.SirketID,
                    MusteriID = fatura.Musteri.MusteriID,
                    Durum = fatura.Durum,
                };
                faturalarList.Add(showFatura);
            }
            return faturalarList;
        }

        public FaturalarGetModel GetFaturaById(int id)
        {
            var fatura = _dbContext.Faturalar.Include(f => f.Musteri).Include(f => f.Sirket).FirstOrDefault(m => m.FaturaID == id);
            if (fatura != null)
            {
                var showFatura = new FaturalarGetModel
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
                    SirketID = fatura.Sirket.SirketID,
                    MusteriID = fatura.Musteri.MusteriID,
                    Durum = fatura.Durum,
                };
                return showFatura;
            }
            return null;
        }

        public string UpdateFatura(int id, FaturalarCreatAndUpdateModel fatura)
        {
            var dbFatura = _dbContext.Faturalar.Include(f => f.Musteri).Include(f => f.Sirket).FirstOrDefault(m => m.FaturaID == id);
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
                    return ("Fatura güncellendi");
                }
                return ("Musteri Bulunamadı");
            }
            return ("Fatura Bulunamadı");

        }

    }
}
