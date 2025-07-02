using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDotNet9.Data;
using WebApiDotNet9.Services.Auditoria;

namespace WebApiDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaInterface _auditoriaInterface;
        public AuditoriaController(IAuditoriaInterface auditoriaInterface)
        {
            _auditoriaInterface = auditoriaInterface;
        }

        [HttpGet("Auditoria")]
        public async Task<IActionResult> BuscarAuditorias()
        {
            var auditoria = await _auditoriaInterface.BuscarAuditorias();
            return Ok(auditoria);
        }
    }
}
