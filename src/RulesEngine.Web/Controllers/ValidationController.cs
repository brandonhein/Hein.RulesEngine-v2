using Hein.RulesEngine.Domain.Magic;
using Hein.RulesEngine.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Web.Controllers
{
    [Route("Validate")]
    public class ValidationController : Controller
    {
        private readonly IAdminToEngineCodeConversion _conversion;
        public ValidationController(IAdminToEngineCodeConversion conversion)
        {
            _conversion = conversion;
        }

        [HttpPost]
        [Consumes("text/plain")]
        [Produces("text/plain")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Index([FromBody] string code)
        {
            var adminCode = this.Request.ReadAsStringAsync().Result;
            var engineCode = await _conversion.ConvertAsync(adminCode);
            var result = await engineCode.DoesCompileAsync();

            return new ContentResult()
            {
                Content = engineCode,
                ContentType = "text/plain",
                StatusCode = 200
            };
        }
    }
}
