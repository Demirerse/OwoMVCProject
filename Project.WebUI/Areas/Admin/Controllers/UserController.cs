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
    public class UserController : Controller
    {
        private readonly AppUserRepository _uRep;
        private readonly UserProfileRepository _upRep;
        public UserController()
        {
            _uRep = new AppUserRepository();
            _upRep = new UserProfileRepository();
        }

        // GET: Admin/User/UserList
        public ActionResult UserList()
        {
            AppUserVM apvm = new AppUserVM
            {
                AppUsers = _uRep.GetActives(),
                Profiles = _upRep.GetActives()

            };
            return View(apvm);
        }

        public ActionResult DeleteUser(int id)
        {
            _uRep.Delete(_uRep.Find(id));
            _upRep.Delete(_upRep.Find(id));

            return RedirectToAction("UserList", "User", new { Area = "Admin" });
        }

        public ActionResult UpdateUser(int id)
        {
            AppUserVM apvm = new AppUserVM
            {
                AppUser = _uRep.Find(id),
                Profile = _upRep.Find(id)
            };
            return View(apvm);
        }

        [HttpPost]
        public ActionResult UpdateUser(AppUserVM apvm)
        {
            //todo validation düzelt
            //if (!ModelState.IsValid) return View();

            AppUser toBeUpdatedUser = _uRep.FirstOrDefault(x => x.ID == apvm.AppUser.ID);
            UserProfile toBeUpdatedUserProfile = _upRep.FirstOrDefault(x => x.ID == apvm.Profile.ID);
            toBeUpdatedUser.UserName = apvm.AppUser.UserName;
            toBeUpdatedUser.Email = apvm.AppUser.Email;
            //todo mail aktivasyonu eklemek mantıklı mı? Sor
            toBeUpdatedUser.Role = apvm.AppUser.Role;

            toBeUpdatedUserProfile.FirstName = apvm.Profile.FirstName;
            toBeUpdatedUserProfile.LastName = apvm.Profile.LastName;
            toBeUpdatedUserProfile.Address = apvm.Profile.Address;


            _uRep.Update(toBeUpdatedUser);
            _upRep.Update(toBeUpdatedUserProfile);

            TempData["updateUser"] = $"{toBeUpdatedUserProfile.FirstName} {toBeUpdatedUserProfile.LastName} kişisi başarıyla güncellenmiştir";


            return RedirectToAction("UserList", "User", new { Area = "Admin" });
        }
    }
}