namespace FaturaTakipAPI.Models.Response
{
    public class FaturalarGetModel
    {
        public string? FaturaNo { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public string? SiparisNo { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public string? Urun { get; set; }
        public int? Miktar { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? KDV { get; set; }
        public decimal? KDVOrani { get; set; }
        public decimal? OdenecekTutar { get; set; }
        public int? SirketID { get; set; }
        public int? MusteriID { get; set; }
        public string? MusteriFullAd { get; set; }
        public bool? Durum { get; set; }
    }
}
