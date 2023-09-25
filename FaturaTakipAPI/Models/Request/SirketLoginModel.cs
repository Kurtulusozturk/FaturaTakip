namespace FaturaTakipAPI.Models.Request
{
    public class SirketLoginModel
    {
        public string? status {  get; set; }
        public string? email { get; set; }
        public string? sifre { get; set; }
        public DateTime? lastLogin { get; set; }
        public string? token { get; set; }
    }
}
