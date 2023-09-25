using FaturaTakip.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FaturaTakipAPI.Models.Response.Musteri;

namespace FaturaTakipAPI.Models.Response
{
    public class MusterilerGetModel
    {
        public int MusteriID { get; set; }
        public string? Ad { get; set; }

        public string? Soyad { get; set; }

        public string? Adres { get; set; }

        public string? Eposta { get; set; }

        public string? TelefonNo { get; set; }

        public List<MusteriIdSiparisNoModel>? musteriIdAndSiparisNo { get; set; }

        public bool Durum { get; set; }
    }
}
