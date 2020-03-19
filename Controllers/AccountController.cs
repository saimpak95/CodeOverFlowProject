using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeOverFlowProject.CustomFilters;
using CodeOverFlowProject.ServiceLayer;
using CodeOverFlowProject.ViewModels;

namespace CodeOverFlowProject.Controllers
{
    public class AccountController : Controller
    {

        IUsersService us;
        public AccountController(IUsersService us)
        {
            this.us = us;
        }
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            
            if (ModelState.IsValid)
            {
             int UserID= this.us.InsertUser(rvm);
                Session["CurrentUserID"] = UserID;
                Session["CurrentUserName"] = rvm.UserName;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ModelState.AddModelError("x", "Invalid");
                return View();
            }
           
        }

        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
               UserViewModel uvm= this.us.GetUserByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.UserName;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = false;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email or Password");
                    return View(lvm);
                }

            }
            else
            {
                ModelState.AddModelError("x", "Invalid Email or Password");
                return View(lvm);
            }
          
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [UserAuthorizationFilter]
        public ActionResult ChangeProfile()
        {
            int UserID = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel user = this.us.GetUserByID(UserID);
            EditUserDetailModel eudm = new EditUserDetailModel { UserName = user.UserName, Email = user.Email, Mobile = user.Mobile, UserID = user.UserID };
            return View(eudm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangeProfile(EditUserDetailModel usvm)
        {
            if (ModelState.IsValid)
            {        
                int UserID = Convert.ToInt32(Session["CurrentUserID"]);
                usvm.UserID = UserID;
                this.us.UpdateUserDetails(usvm);
                Session["CurrentUserName"] = usvm.UserName;
            }
            return RedirectToAction("Index", "Home");
            
        }

        [UserAuthorizationFilter]
        public ActionResult ChangePassword()
        {
            int UserID = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel user = this.us.GetUserByID(UserID);
            EditUserPasswordViewModel eupvm = new EditUserPasswordViewModel {  Email = user.Email,Password=user.Password, UserID = user.UserID };
            return View(eupvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(EditUserPasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                int UserID = Convert.ToInt32(Session["CurrentUserID"]);
                eupvm.UserID = UserID;
                this.us.UpdateUserPassword(eupvm);
                Session["CurrentUserPassword"] = eupvm.Password;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(eupvm);
            }
           

        }
    }
}