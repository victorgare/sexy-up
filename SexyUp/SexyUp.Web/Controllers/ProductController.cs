using SexyUp.ApplicationCore.Constants;
using System.Web.Mvc;

namespace SexyUp.Web.Controllers
{
    [Authorize(Roles = Roles.AdministradorAndFornecedor), ValidateAntiForgeryToken]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}