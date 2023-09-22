using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
using FaturaTakipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaturaTakipAPI.Controllers
{
    [Route("api/fatura")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _faturaService;
        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }
        [HttpGet("getall")]
        public IActionResult GetAllFaturalar()
        {
            var faturalar = _faturaService.GetAllFaturalar();
            return Ok(faturalar);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetFaturaById(int id)
        {
            var fatura = _faturaService.GetFaturaById(id);
            if (fatura == null)
            {
                return NotFound();
            }
            return Ok(fatura);
        }

        [HttpPost("create")]
        public IActionResult CreateFatura([FromBody] FaturalarCreatAndUpdateModel fatura)
        {
            _faturaService.CreateFatura(fatura);
            return Ok("Fatura oluşturuldu");
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateFatura(int id, [FromBody] FaturalarCreatAndUpdateModel fatura)
        {
            _faturaService.UpdateFatura(id,fatura);
            return Ok("Fatura güncellendi");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteFatura(int id)
        {
            _faturaService.DeleteFatura(id);
            return Ok(_faturaService.GetFaturaById(id));
        }
    }
}
