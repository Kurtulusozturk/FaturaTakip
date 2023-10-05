namespace FaturaTakip.Models.Account.request
{
    public class RegisterRequestModel
    {
        public string SirketAdı { get; set; }
        public string Adres { get; set; }
        public string TelefonNo { get; set; }
        public string WebAdresi { get; set; }
        public string Eposta { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiKimlikNo { get; set; }
        public string Sifre { get; set; }
        public bool Durum { get; set; }
    }
}
