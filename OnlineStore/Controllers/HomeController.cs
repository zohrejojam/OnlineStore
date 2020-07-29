using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMaterial()
        {
            return PartialView();
        }


        public ActionResult AccountingDocument()
        {
            return PartialView();
        }
        public ActionResult ListMaterial()
        {
            return PartialView();
        }
    }
}