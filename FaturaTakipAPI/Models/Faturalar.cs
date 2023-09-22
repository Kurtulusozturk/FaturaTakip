using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace FaturaTakip.Models
{
    public class Faturalar
    {
        [Key]
        public int FaturaID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string? FaturaNo { get; set; }
        public DateTime FaturaTarihi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string? SiparisNo { get; set; }
        public DateTime SiparisTarihi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string? Urun { get; set; }
        public int? Miktar { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? KDV { get; set; }
        public decimal? KDVOrani { get; set; }
        public decimal? OdenecekTutar { get; set; }
        public virtual Sirketler? Sirket { get; set; }
        public virtual Musteriler? Musteri { get; set; }
        public bool? Durum { get; set; }
    }

}