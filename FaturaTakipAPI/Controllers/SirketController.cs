using FaturaTakip.Models;
using FaturaTakipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaturaTakipAPI.Controllers
{
    [Route("api/sirket")]
    [ApiController]
    public class SirketController :ControllerBase
    {
        private readonly ISirketService _sirketService;
        public SirketController(ISirketService sirketService)
        {
            _sirketService = sirketService;
        }
        [HttpGet("getbyid/{id}")]
        public IActionResult GetSirketById(int id)
        {
            var sirket = _sirketService.GetSirketById(id);
            if (sirket == null)
            {
                return NotFound();
            }
            return Ok(sirket);
        }
        [HttpGet("getbyemail/{email}")]
        public IActionResult GetSirketByEmail(string email)
        {
            var sirket = _sirketService.GetSirketByEmail(email);
            if (sirket == null)
            {
                return NotFound();
            }
            return Ok(sirket);
        }

        [HttpPost("create")]
        public IActionResult CreateSirket([FromBody] Sirketler sirket)
        {
            return NoContent();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateSirket(int id, [FromBody] Sirketler sirket)
        {
            return NoContent();
        }
    }
}
