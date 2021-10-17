using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WebUI.Models.ShoppingTools;
using Project.WebUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppUserRepository _apRep;
        private readonly UserProfileRepository _proRep;

        public RegisterController()
        {
            _apRep = new AppUserRepository();
            _proRep = new UserProfileRepository();
        }

        // GET: RegisterNow
        public ActionResult RegisterNow() => View();

        //post / registerNow
        [HttpPost]
        public ActionResult RegisterNow(AppUserVM apvm)
        {
            if (!ModelState.IsValid) return View();


            AppUser appUser = apvm.AppUser;
            UserProfile profile = apvm.Profile;

            appUser.Password = DantexCrypt.Crypt(appUser.Password); //sifreyi kriptoladık

            if (_apRep.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce kullanılmış, lütfen başka bir isim seçiniz.";
                return View();
            }
            else if (_apRep.Any(x => x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Email daha önce kullanılmış, lütfen başka bir Email seçiniz.";
                return View();
            }

            //Kullanıcı basarılı bir şekilde Register işlemini tamamladıysa ona Mail gönderecegiz... https://localhost:44373/
            
            string gonderilecekEmail = "Tebrikler... Hesabınız oluşturulmuştur...Hesabınızı aktive etmek için https://localhost:44373/Register/Activation/" + appUser.ActivationCode + " linkine tıklayabilirsiniz. ";

            MailSender.Send(appUser.Email, body: gonderilecekEmail, subject: "Hesap aktivasyon!");

            _apRep.Add(appUser); //profilden önce bunu eklemelisiniz önceliginiz bunu eklemek olmalı..Cünkü AppUser'in ID'si ilk basta olusmalı...Cünkü bizim kurdugumuz birebir ilişkide AppUser zorunlu olan alan Profile ise opsiyonel alandır. Dolayısıyla ilk basta AppUser'in ID'si SaveChanges ile olusmalı ki Profile'i rahatca ekleyebilelim(eger ekleyeceksek)

            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()) || !string.IsNullOrEmpty(profile.Address.Trim()))
            {
                profile.ID = appUser.ID;
                _proRep.Add(profile);
            }



            return View("RegisterOk");
        }

        //get / activation
        public ActionResult Activation(Guid id)
        {
            AppUser aktifEdilecek = _apRep.FirstOrDefault(x => x.ActivationCode == id);
            if (aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                _apRep.Update(aktifEdilecek);
                TempData["HesapAktifMi"] = "Hesabınız aktif hale getirildi";
                if (Session["bekleyenUrun"] != null)
                {
                    Session["scart"] = ControlProduct.KeepProduct(Session["bekleyenUrun"]);
                }
                return RedirectToAction("ShoppingList","Shopping");
            }
            else
                TempData["HesapAktifMi"] = "Hesabınız bulunamadı";
            return RedirectToAction("Login", "Home");
        }
        //get / registerOk
        public ActionResult RegisterOk() => View();

        //get / logOut
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}