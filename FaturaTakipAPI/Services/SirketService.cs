using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
using FaturaTakipAPI.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FaturaTakipAPI.Services
{
    public interface ISirketService
    {
        SirketlerGetModel GetSirketById(int id);
        SirketLoginModel GetSirketByEmail(string email, string sifre);
        string CreateSirket(SirketlerCreateAndUpdateModel sirket);
        string UpdateSirket(int id, SirketlerCreateAndUpdateModel sirket);
        bool ValidateToken(string token);
    }
    public class SirketService : ISirketService
    {
        string signingKey = "ThisIsPrivateKeyWhichIsSolveJwt";
        // Burada DbContext veya başka bir veritabanı erişimi için gerekli kodlar olur.
        private readonly FaturaTakipDbContext _dbContext;
        public SirketService(FaturaTakipDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string CreateSirket(SirketlerCreateAndUpdateModel sirket)
        {
            //jwt
            var newSirket = new Sirketler
            {
                SirketAdı = sirket.SirketAdı,
                Adres = sirket.Adres,
                TelefonNo = sirket.TelefonNo,
                WebAdresi = sirket.WebAdresi,
                Eposta = sirket.Eposta,
                VergiDairesi = sirket.VergiDairesi,
                VergiKimlikNo = sirket.VergiKimlikNo,
                Sifre = sirket.Sifre,
                Musteri = null,
                Fatura = null,
                Durum = sirket.Durum
            };

            _dbContext.Sirketler.Add(newSirket);
            _dbContext.SaveChanges();
            return ("Yeni sirket oluşturuldu");
        }

        public SirketLoginModel GetSirketByEmail(string email , string sifre)
        {
            //jwt
            var sirket = _dbContext.Sirketler.Include(f => f.Musteri).Include(f => f.Fatura).SingleOrDefault(m => m.Eposta == email && m.Sifre == sifre);
            if (sirket == null)
            {
                return null;
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, email)
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    // tokeni sağlayan kişi issuer:"asdasdas.com"
                    //audience:""
                    //claims :token içinde tuttulan veri
                    claims: claims,
                    //token ömrü
                    expires: DateTime.Now.AddHours(1),
                    //token ömrü ne zaman bitecek
                    notBefore:DateTime.Now,
                    //token çözmek için key
                    signingCredentials: credentials
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                var showSirket = new SirketLoginModel
                {
                    email = sirket.Eposta,
                    token = token,
                    id = sirket.SirketID
                };
                return showSirket;
            }
        }

        public SirketlerGetModel GetSirketById(int id)
        {
            var sirket = _dbContext.Sirketler.Include(f => f.Musteri).Include(f => f.Fatura).FirstOrDefault(m => m.SirketID == id);
            return GetSirketlerShortCut(sirket);
        }

        public string UpdateSirket(int id, SirketlerCreateAndUpdateModel sirket)
        {
            var dbSirket = _dbContext.Sirketler.Include(f => f.Musteri).Include(f => f.Fatura).FirstOrDefault(m => m.SirketID == id);
            if (dbSirket != null)
            {
                var sifre = BCrypt.Net.BCrypt.EnhancedHashPassword(sirket.Sifre, 13);
                dbSirket.SirketAdı = sirket.SirketAdı;
                dbSirket.Adres = sirket.Adres;
                dbSirket.TelefonNo = sirket.TelefonNo;
                dbSirket.WebAdresi = sirket.WebAdresi;
                dbSirket.Eposta = sirket.Eposta;
                dbSirket.VergiDairesi = sirket.VergiDairesi;
                dbSirket.VergiKimlikNo = sirket.VergiKimlikNo;
                dbSirket.Sifre = sifre;
                dbSirket.Durum = sirket.Durum;
                _dbContext.SaveChanges();
                return ("Sirket güncellendi");
            }
            return ("Sirket Bulunamadı");

        }
        public bool ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            try
            {
                JwtSecurityTokenHandler handler = new ();
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,

                },out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims.ToList();
                return true;
            }catch (System.Exception)
            {
                return false;
            }
        }
        public SirketlerGetModel GetSirketlerShortCut(Sirketler sirket)
        {
            if (sirket == null)
            {
                return null;
            }
            else
            {
                var listOfFaturaID = new List<int>();
                var listOfMusteriID = new List<int>();
                foreach (var item in sirket.Fatura)
                {
                    listOfFaturaID.Add(item.FaturaID);
                }
                foreach (var item in sirket.Musteri)
                {
                    listOfMusteriID.Add(item.MusteriID);
                }
                var showSirket = new SirketlerGetModel
                {
                    SirketAdı = sirket.SirketAdı,
                    Adres = sirket.Adres,
                    TelefonNo = sirket.TelefonNo,
                    WebAdresi = sirket.WebAdresi,
                    Eposta = sirket.Eposta,
                    VergiDairesi = sirket.VergiDairesi,
                    VergiKimlikNo = sirket.VergiKimlikNo,
                    Sifre = sirket.Sifre,
                    MusteriIDs = listOfMusteriID,
                    FaturaIDs = listOfFaturaID,
                    Durum = sirket.Durum,

                };
                return showSirket;
            }
        }
    }
}
