
using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.WebUI.AuthenticationClasses;
using Project.WebUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _cRep;

        public CategoryController()
        {
            _cRep = new CategoryRepository();
        }

        // GET: Admin/Category
        public ActionResult CategoryList(int? id)
        {

            CategoryVM cvm = id == null ? new CategoryVM
            {
                Categories = _cRep.GetActives()
            } : new CategoryVM { Categories = _cRep.Where(x => x.ID == id) };


            return View(cvm);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid) return View();
            _cRep.Add(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult UpdateCategory(int id)
        {
            if (!ModelState.IsValid) return View();

            CategoryVM cvm = new CategoryVM
            {
                Category = _cRep.Find(id)
            };
            return View(cvm);

        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            _cRep.Update(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            _cRep.Delete(_cRep.Find(id));
            return RedirectToAction("CategoryList");
        }
    }
}