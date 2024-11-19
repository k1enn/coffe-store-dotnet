using System.Web.Mvc;

namespace BoysCoffe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {          
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
    }
}