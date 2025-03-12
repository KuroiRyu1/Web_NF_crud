using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using Web_NF_crud.Models.ModelView;
using Web_NF_crud.Models.Repositories;

namespace Web_NF_crud.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            HashSet<CategoryView> list;
            var q = CategoryRepository.Instance.All();
            list = q ?? new HashSet<CategoryView>();
            ViewBag.data = list;
            ViewBag.msg = -1;
            var res = Request.QueryString["msg"];
            if ((res!=null))
            {
                ViewBag.msg = res;
            }

            return View();
        }
        public ActionResult newCate()
        {
            HashSet<CategoryView> view = CategoryRepository.Instance.All();
            ViewData["view"] = view;
            return View();
        }
        public ActionResult Create(CategoryView model)
        {
            if (model != null)
            {
                CategoryRepository.Instance.create(model);
            }
            return RedirectToAction("index", "Category");
        }
        public ActionResult Edit()
        {
            try
            {
                int id = -1;
                if (Request.QueryString["cate_id"] != null)
                {
                    if (int.TryParse(Request.QueryString["cate_id"], out id) == false)
                    {
                        return RedirectToAction("index");
                    }
                    id = int.Parse(Request.QueryString["cate_id"]);
                    ViewBag.data = CategoryRepository.Instance.findById(id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return View();
        }
        public ActionResult Edit2(CategoryView model)
        {
            if (model != null)
            {
                CategoryRepository.Instance.update(model);
            }
            return RedirectToAction("index");
        }
        public ActionResult Delete()
        {
            int a = int.Parse(Request.QueryString["cate_id"]);
            if (Request.QueryString["cate_id"] != null)
            {
                int result = CategoryRepository.Instance.delete(CategoryRepository.Instance.findById(a));
                return RedirectToAction("index", new { msg = result });

            }

            return RedirectToAction("index");
        }
        public ActionResult UpdateActive()
        {
            int id =int.Parse(Request.Params["cate_id"]);
            int active = int.Parse(Request.Params["cate_checked"]);
            string msg = CategoryRepository.Instance.Active(id,active);
            return Json(msg);
        }
    }
}