using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
using FaturaTakipAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaturaTakipAPI.Controllers
{
    [Route("api/musteri")]
    [ApiController]
    [Authorize]
    //ControllerBase kütüphanesi, ASP.NET Core web uygulamalarının denetleyicilerinin temel sınıfını ifade eder ve bu sınıf,
    //MVC (Model-View-Controller) veya Web API uygulamalarında kullanılır.
    //ControllerBase sınıfı, bir denetleyici (controller) sınıfının türetilmesi için temel sağlar
    //ve çeşitli özellikler ve metotlar içerir.
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;

        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        [HttpGet("getbysirketid/{id}")]
        public IActionResult GetMusteriBySirketId(int id)
        {
            var musteriler = _musteriService.GetMusterilerBySirketId(id);
            return Ok(musteriler);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetMusteriById(int id)
        {
            var musteri = _musteriService.GetMusteriById(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return Ok(musteri);
        }

        [HttpPost("create")]
        public IActionResult CreateMusteri([FromBody] MusterilerCreateAndUpdateModel musteri)
        {
            _musteriService.CreateMusteri(musteri);
            return CreatedAtAction(nameof(GetMusteriById), new { id = musteri.MusteriID }, musteri);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateMusteri(int id, [FromBody] MusterilerCreateAndUpdateModel musteri)
        {
            return Ok(_musteriService.UpdateMusteri(id, musteri));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMusteri(int id)
        {
            _musteriService.DeleteMusteri(id);
            return Ok(_musteriService.GetMusteriById(id));
        }
    }
}
