using FaturaTakip.Models;
using FaturaTakipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaturaTakipAPI.Controllers
{
    [Route("api/musteri")]
    [ApiController]
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;

        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        [HttpGet]
        public IActionResult GetAllMusteriler()
        {
            var musteriler = _musteriService.GetAllMusteriler();
            return Ok(musteriler);
        }

        [HttpGet("{id}")]
        public IActionResult GetMusteriById(int id)
        {
            var musteri = _musteriService.GetMusteriById(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return Ok(musteri);
        }

        [HttpPost]
        public IActionResult CreateMusteri([FromBody] Musteriler musteri)
        {
            _musteriService.CreateMusteri(musteri);
            return CreatedAtAction(nameof(GetMusteriById), new { id = musteri.MusteriID }, musteri);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMusteri(int id, [FromBody] Musteriler musteri)
        {
            if (id != musteri.MusteriID)
            {
                return BadRequest();
            }
            _musteriService.UpdateMusteri(musteri);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMusteri(int id)
        {
            _musteriService.DeleteMusteri(id);
            return Ok(_musteriService.GetMusteriById(id));
        }
    }
}
