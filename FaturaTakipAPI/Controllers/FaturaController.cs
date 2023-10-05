using FaturaTakip.Models;
using FaturaTakipAPI.Models.Request;
using FaturaTakipAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaturaTakipAPI.Controllers
{
    [Route("api/fatura")]
    [ApiController]
    [Authorize]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _faturaService;
        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }
        [HttpGet("getbysirketbyid/{id}")]
        public IActionResult GetFaturalarBySirketID(int id)
        {
            var faturalar = _faturaService.GetFaturalarBySirketID(id);
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
            return Ok(_faturaService.CreateFatura(fatura));
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateFatura(int id, [FromBody] FaturalarCreatAndUpdateModel fatura)
        {
            return Ok(_faturaService.UpdateFatura(id, fatura));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteFatura(int id)
        {
            _faturaService.DeleteFatura(id);
            return Ok(_faturaService.GetFaturaById(id));
        }
    }
}
