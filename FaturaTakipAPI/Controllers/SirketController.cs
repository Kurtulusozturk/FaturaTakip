using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
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
        public IActionResult GetSirketByEmail(string email, string sifre)
        {
            var sirket = _sirketService.GetSirketByEmail(email , sifre);
            if (sirket == null)
            {
                return NotFound();
            }
            return Ok(sirket);
        }

        [HttpPost("create")]
        public IActionResult CreateSirket([FromBody] SirketlerCreateAndUpdateModel sirket)
        {
            return Ok(_sirketService.CreateSirket(sirket));
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateSirket(int id, [FromBody] SirketlerCreateAndUpdateModel sirket)
        {
            return Ok(_sirketService.UpdateSirket(id, sirket));
        }
    }
}
