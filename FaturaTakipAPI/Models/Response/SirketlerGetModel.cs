using FaturaTakip.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FaturaTakipAPI.Models.Response
{
    public class SirketlerGetModel
    {
        public string? SirketAdi { get; set; }
        public string? Adres { get; set; }
        public string? TelefonNo { get; set; }
        public string? WebAdresi { get; set; }
        public string? Eposta { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiKimlikNo { get; set; }
        public string? Sifre { get; set; }
        public List<int> MusteriIDs { get; set; }
        public List<int> FaturaIDs { get; set; }
        public bool Durum { get; set; }
    }
}
