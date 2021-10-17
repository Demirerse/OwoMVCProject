using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WebUI.Models.ShoppingTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppUserRepository _apRep;
        public HomeController()
        {
            _apRep = new AppUserRepository();
        }
        //get / index
        public ActionResult Index() => View();

        //get / login
        public ActionResult Login() => View();

        //post / login 
        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            

            AppUser yakalanan = _apRep.FirstOrDefault(x => x.UserName == appUser.UserName);
            if (yakalanan==null)
            {
                ViewBag.Kullanici = "Kullanıcı bulunamadı";
                return View();
            }

            string decrypted = DantexCrypt.DeCrypt(yakalanan.Password);
            if (appUser.Password==decrypted && yakalanan.Role==ENTITIES.Enums.UserRole.Admin)
            {
                if (!yakalanan.Active)
                {
                    return AktifKontrol();
                }
                Session["admin"] = yakalanan;
                return RedirectToAction("CategoryList", "Category", new { Area = "Admin" });
            }
            else if (yakalanan.Role==ENTITIES.Enums.UserRole.Member && appUser.Password==decrypted)
            {
                if (!yakalanan.Active)
                {
                    return AktifKontrol();
                }
                Session["member"] = yakalanan;
                if (Session["bekleyenUrun"] != null)
                {
                    Session["scart"] = ControlProduct.KeepProduct(Session["bekleyenUrun"]);

                }
                return RedirectToAction("ShoppingList", "Shopping");
            }
            ViewBag.Kullanici = "Kullanici bulunamadı";
            return View();

        }
        //get / aktifKontrol
        private ActionResult AktifKontrol()
        {
            ViewBag.AktifDegil = "Lutfen hesabınızı aktif hale getiriniz..Mailinizi kontrol ediniz";
            return View("Login");
        }

    }
}