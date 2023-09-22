using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaturaTakip.Models
{
    public class Musteriler
    {
        [Key]
        public int MusteriID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string? Ad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string? Soyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string? Adres { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string? Eposta { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string? TelefonNo { get; set; }

        public ICollection<Faturalar>? Fatura { get; set; }
        public virtual Sirketler? Sirket { get; set; }
        public bool Durum { get; set; }

    }
}