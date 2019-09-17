using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.REpository;
using GkhanOrganization.Models;
using GkhanOrganization.MyActionFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GkhanOrganization.Controllers
{
    public class HomeController : Controller
    {
        static OrganizationRepository _Orgrep;
        static UserRepository _userrep;
        static OrganizationMemberRepository _Orgmemrep;
        static CommentRepository _comRep;
        public HomeController(OrganizationRepository orgrep, UserRepository userrep, OrganizationMemberRepository orgmemrep, CommentRepository commentrepository)
        {
            _Orgrep = orgrep;
            _userrep = userrep;
            _Orgmemrep = orgmemrep;
            _comRep = commentrepository;
        }
        public ActionResult Index()
        {
            List<Organization> list = _Orgrep.Listele().OrderBy(c => c.OrgDate).Take(8).ToList();

            return View(list);
        }

        public ActionResult Organizasyonlar()
        {
            var list = _Orgrep.Listele().Where(c=>c.OrgDate>DateTime.Now).OrderBy(c => c.OrgDate).ToList();
            return View(list);
        }
        [LogIn]
        public ActionResult Memberlar()
        {
            return View(_userrep.Listele());
        }
        [AdminOnly]
        public ActionResult OrganizasyonAdd()
        {
            ViewData["Userlar"] = _userrep.Listele();

            return View();
        }
        [HttpPost]
        public ActionResult OrganizasyonAdd(OrganizationModelview entity)
        {
            Organization org = new Organization();
            org.Name = entity.Name;
            org.Description = entity.Description;
            org.OrgDate = entity.OrgDate;
            org.Place = entity.Place;
            org.Organizer = _userrep.Find(entity.Organizer_Id);

            _Orgrep.Add(org);

            org.ImageUrl = org.Name + "_" + org.Id + ".jpg";

            _Orgrep.Update(org);

            string imageway = ConfigurationManager.AppSettings["ImagePath"] + "/" + org.ImageUrl;

            Request.Files[0].SaveAs(imageway);


            return RedirectToAction("Organizasyonlar");
        }
        [AdminOnly]
        public ActionResult DeleteOrganization(int id)
        {
            _Orgrep.Delete(id);
            return RedirectToAction("Organizasyonlar");
        }
        [AdminOnly]
        public ActionResult OrganizationEdit(int id)
        {
            ViewData["Userlar"] = _userrep.Listele();
            Organization org = _Orgrep.Find(id);
            return View(org);
        }
        [HttpPost]
        public ActionResult OrganizationEdit(OrganizationModelview entity)
        {
            Organization org = _Orgrep.Find(entity.Id);

            #region Eski Resimleri Silmek için Yazdım
            string fullPath = Request.MapPath("~/Images/" + org.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            #endregion


            org.Name = entity.Name;
            org.Description = entity.Description;
            org.OrgDate = entity.OrgDate;
            org.Place = entity.Place;
            org.Organizer = _userrep.Find(entity.Organizer_Id);

            org.ImageUrl = org.Name + "_" + org.Id + ".jpg";

            _Orgrep.Update(org);

            string imageway = ConfigurationManager.AppSettings["ImagePath"] + "/" + org.ImageUrl;

            Request.Files[0].SaveAs(imageway);


            return RedirectToAction("Organizasyonlar");
        }
        [LogIn]
        public ActionResult OrganizationDetails(int id)
        {
        
            return View(_Orgrep.Find(id));
        }
        
        public PartialViewResult OrganizationMemberList(int id)
        {
            List<OrganizationMember> orgmemList = _Orgmemrep.Listele().Where(c => c.Organization.Id == id).ToList();
            List<User> userlist = _userrep.Listele();

            foreach (var item in orgmemList)
            {
                foreach (var usr in userlist)
                {
                    if (usr.Id == item.Users.Id)
                    {
                        userlist.Remove(usr);
                        break;
                    }
                }
            }

            ViewData["Userlar"] = userlist;
            ViewData["Organizer_Id"] = id;

        DbOrganization db = new DbOrganization();
            var list = db.OrganizationMembers.Where(c => c.Organization.Id == id).ToList();
            return PartialView(list);
        }
        [AdminOnly]
        public void UserEkleOrga(int User_Id, int Org_Id)
        {
            OrganizationMember orgmem = new OrganizationMember();
            orgmem.Organization = _Orgrep.Find(Org_Id);
            orgmem.Users = _userrep.Find(User_Id);
            _Orgmemrep.Add(orgmem);

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {
            if (!ModelState.IsValid)
            {
                return View(lm);
            }

            List<User> usrelist = _userrep.Listele();

            foreach (var item in usrelist)
            {
                if (item.UserName==lm.UserName&&item.Password==lm.Password)
                {
                    Session["UserLogin"] = item;
                    return Redirect(Request.UrlReferrer.ToString()); ///burada previous sayfaya gidilmeye calsıldı
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session["UserLogin"] = null;
            return RedirectToAction("Login");
        }
        public void Kaldir(int User_Id, int Org_Id)
        {
            DbOrganization db = new DbOrganization();
            OrganizationMember orgmem= db.OrganizationMembers.Where(c => c.Users.Id == User_Id && c.Organization.Id == Org_Id).FirstOrDefault();
            _Orgmemrep.Delete(orgmem.Id);

        }
        public ActionResult BitenOrganizasyonlar()
        {
            List<Organization> liste= _Orgrep.Listele().Where(c => c.OrgDate < DateTime.Now).OrderByDescending(c=>c.OrgDate).ToList();

            return View(liste);
        }
        
        public PartialViewResult BitenOrganizasyonPartial(int id)
        {
            ViewData["Org_Id"] = id;
            ViewData["CommentList"] = _Orgrep.Find(id).OrganizasyonYorumlar;

            DbOrganization db = new DbOrganization();
                var list = db.OrganizationMembers.Where(c => c.Organization.Id == id).ToList();
                return PartialView(list);
        }
        public void YorumEkle(string comment,int User_id, int Org_Id)
        {
            Comment cmt = new Comment();
            cmt.Yorum = comment;
            cmt.OrganizationYorum = _Orgrep.Find(Org_Id);
            cmt.UserYorum = _userrep.Find(User_id);
            cmt.YorumTime = DateTime.Now;
            _comRep.Add(cmt);
        }

    }
}