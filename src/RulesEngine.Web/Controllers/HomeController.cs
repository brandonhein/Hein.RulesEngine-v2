using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hein.RulesEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return Redirect("/swagger");
        }
    }
}
