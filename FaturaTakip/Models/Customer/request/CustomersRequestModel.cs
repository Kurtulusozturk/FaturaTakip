namespace FaturaTakip.Models.Customer.request
{
	public class CustomersRequestModel
	{
		public string Ad { get; set; }

		public string Soyad { get; set; }

		public string Adres { get; set; }

		public string Eposta { get; set; }

		public string TelefonNo { get; set; }
		public int SirketID { get; set; }

		public int FaturaID { get; set; }

		public bool Durum { get; set; }
	}
}
