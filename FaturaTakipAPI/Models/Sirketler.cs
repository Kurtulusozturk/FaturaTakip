using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaturaTakip.Models
{
    public class Sirketler
    {
        [Key]
        public int SirketID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string? SirketAdi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string? Adres { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string? TelefonNo { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        public string? WebAdresi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string? Eposta { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string? VergiDairesi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string? VergiKimlikNo { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string? Sifre { get; set; }

        public  ICollection<Musteriler>? Musteri { get; set; }
        public  ICollection<Faturalar>? Fatura { get; set; }
        public bool Durum { get; set; }
    }
}