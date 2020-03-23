using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosDataModel;
using PosRepository;

namespace PosWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Category> list = CategoryRepo.All();
            return PartialView("_List", list);
        }

        //Get
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            ResponseResult result = CategoryRepo.Update(model);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                },
                JsonRequestBehavior.AllowGet);
        }

        //Get
        public ActionResult Edit(long id)
        {
            //Cara 1
            //Category category = CategoryRepo.ById(id);
            //return PartialView("_Edit", category);

            //Cara 2
            return PartialView("_Edit", CategoryRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            ResponseResult result = CategoryRepo.Update(model);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message
                },
                JsonRequestBehavior.AllowGet);
        }

        //Get
        public ActionResult Delete(long id)
        {
            return PartialView("_Delete", CategoryRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(Category model)
        {
            ResponseResult result = CategoryRepo.Delete(model.Id);
            return Json(
                new
                {
                    success = result.Success,
                    message = result.Message
                },
                JsonRequestBehavior.AllowGet);

        }

    }
}
