namespace FaturaTakipAPI.Models.Response.Musteri
{
    public class MusteriIdSiparisNoModel
    {
        public int? FaturaID { get; set; }

        public string? SiparisNo { get; set; }

        public static implicit operator List<object>(MusteriIdSiparisNoModel v)
        {
            throw new NotImplementedException();
        }
    }
}
