namespace FaturaTakip.Models.Customer.response
{
	public class CustomersResponseModel
	{
		public int MusteriID { get; set; }
		public string Ad { get; set; }

		public string Soyad { get; set; }

		public string Adres { get; set; }

		public string Eposta { get; set; }

		public string TelefonNo { get; set; }

		public List<MusteriIdSiparisNoModel> musteriIdAndSiparisNo { get; set; }

		public bool Durum { get; set; }
	}
}
