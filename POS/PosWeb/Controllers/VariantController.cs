using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosDataModel;
using PosRepository;

namespace PosWeb.Controllers
{
    public class VariantController : Controller
    {
        // GET: Variant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Variant> list = VariantRepo.All();
            return PartialView("_List", list);
        }

        //Get
        public ActionResult Create()
        {
            // supaya ga diisi manual, tp hanya pilihan dari category yg dibuat sebelumnya
            //Parent List ==> Category
            ViewBag.ParentList = new SelectList(CategoryRepo.All(), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Variant model)
        {
            ResponseResult result = VariantRepo.Update(model);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ByCategory(long id = 0)
        {
            // id => Category Id
            List<Variant> list = VariantRepo.ByCategory(id);
            return PartialView("_ByCategory", list);
        }
    }
}
