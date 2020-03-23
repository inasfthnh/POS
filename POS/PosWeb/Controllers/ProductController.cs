using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosDataModel;
using PosRepository;

namespace PosWeb.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Product> list = ProductRepo.All();
            return PartialView("_List", list);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Initial");
            ViewBag.VariantList = new SelectList(VariantRepo.ByCategory(-1), "Id", "Initial");
            return PartialView("_Create");
        }
    }
}
