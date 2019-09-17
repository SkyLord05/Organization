using DataAccessLayer.Entities;
using DataAccessLayer.REpository;
using GkhanOrganization.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GkhanOrganization.Controllers
{
    public class UserController : Controller
    {
        static UserRepository _usrep;
        public UserController(UserRepository usrep)
        {
            _usrep = usrep;

        }
        public ActionResult UserCreate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UserCreate(UserModelView usr)
        {
            if (!ModelState.IsValid)
            {
                return View(usr);
            }

            User uu = new User();
            uu.Age = usr.Age;
            uu.UserName = usr.UserName;
            uu.Password = usr.Password;
            uu.UserType = 1;

            _usrep.Add(uu);


            return RedirectToAction("Login", "Home");
        }
        public ActionResult UserDetail(int id)
        {
            
            return View(_usrep.Find(id));
        }
        public ActionResult UserDelete(int id)
        {
            _usrep.Delete(id);
            return RedirectToAction("Memberlar", "Home");
        }
        public ActionResult UserEdit(int id)
        {
            var usr = _usrep.Find(id);
            UserModelView uu = new UserModelView();
            uu.Age = usr.Age;
            uu.Id = usr.Id;
            uu.Password = usr.Password;
            uu.UserType = usr.UserType;
            uu.UserName = usr.UserName;
            return View(uu);
        }
        [HttpPost]
        public ActionResult UserEdit(UserModelView usr)
        {
            User uu = _usrep.Find(usr.Id);
            uu.UserName = usr.UserName;
            uu.Age = usr.Age;
            uu.Password = usr.Password;
            uu.UserType = usr.UserType;
            _usrep.Update(uu);

            if (((User)Session["UserLogin"]).UserType==0)
            {
                return RedirectToAction("Memberlar", "Home");
            }
            Session["UserLogin"] = null;
            return RedirectToAction("Login", "Home");

        }
    }
}