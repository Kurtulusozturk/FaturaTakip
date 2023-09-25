namespace FaturaTakipAPI.Models.Request
{
    public class MusterilerCreateAndUpdateModel
    {
        public int MusteriID { get; set; }
        public string? Ad { get; set; }

        public string? Soyad { get; set; }

        public string? Adres { get; set; }

        public string? Eposta { get; set; }

        public string? TelefonNo { get; set; }

        public int? FaturaID { get; set; }

        public int? SirketID { get; set; }

        public bool Durum { get; set; }
    }
}
