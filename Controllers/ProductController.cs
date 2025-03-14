using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_NF_crud.Models.ModelView;
using Web_NF_crud.Models.Repositories;

namespace Web_NF_crud.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var item = ProductRepository.Instance.All();
            ViewBag.data = item;
            return View();
        }
        public ActionResult New_Product()
        {
            var item = CategoryRepository.Instance.ListCategoryActive();
            ViewBag.data = item;
            return View();
        }
        public ActionResult create(HttpPostedFileBase Img, ProductView model)
        {
            try
            {
                if (Img != null)
                {
                    string newFileName = $"{DateTime.Now.Ticks.ToString()}{Img.FileName}";
                    string fullPathSave = $"{Server.MapPath(Url.Content("~/content/images"))}\\{newFileName}";
                    Img.SaveAs(fullPathSave);
                    model.ImageName = newFileName;
                    ProductRepository.Instance.create(model);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
    }
}