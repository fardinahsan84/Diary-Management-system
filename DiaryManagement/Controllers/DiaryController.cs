using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiaryManagement.Models;
using DigitalDiary.Repository;

namespace DigitalDiary.Controllers
{
    public class DiaryController : Controller
    {
        IRepository<User> usrRepo = new UserRepository();
        IRepository<Diary> dRepo = new DiaryRepository();

        public ActionResult Index()
        {
            return View(dRepo.GetAll());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public ActionResult Signup(Diary a, HttpPostedFileBase Image,FormCollection Form)
        {
            if (ModelState.IsValid)
            {
                //d = dRepo.Get(id);
                if (Image == null)
                {
                    a.Notes = Form["Notes"];
                    dRepo.Insert(a);
                    return RedirectToAction("Index");
                }
                else
                {
                    string pic = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension = Path.GetExtension(Image.FileName);
                    pic = pic + DateTime.Now.ToString("yymmssfff") + extension;
                    a.Image = "~/Image/" + pic;
                    pic = Path.Combine(Server.MapPath("~/Image/"), pic);
                    Image.SaveAs(pic);
                    a.Notes = Form["Notes"];
                    a.Date = DateTime.Now.ToString("dd/MM/yyyy");

                    dRepo.Insert(a);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("Create");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Signin(string Username, string Password, FormCollection form)
        {
            if (usrRepo.GetAll().Where(c => c.Username == form["Username"].ToString() && c.Password == form["Password"].ToString()).FirstOrDefault() != null)
            {
                var user = usrRepo.GetAll().Where(c => c.Username == form["Username"].ToString() && c.Password == form["Password"].ToString()).FirstOrDefault();
                Session["stid"] = user.UserId;
                List<Diary> list = dRepo.GetAll().Where(c => c.UserId == user.UserId).ToList();
                Session["stId"] = list; 
                return RedirectToAction("Index", "Diary");
            }
            else
            {
                ViewData["invalidlogin"] = "Invalid Login!Try Again";
                return View("Login");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(dRepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection Form,Diary d,HttpPostedFileBase Image,int id)
        {
            if (ModelState.IsValid)
            {
                d = dRepo.Get(id);
                if (Image == null)
                {
                    d.Notes = Form["Notes"];
                    dRepo.Update(d);
                    return RedirectToAction("Index");
                }
                else 
                {
                    string pic = Path.GetFileNameWithoutExtension(Image.FileName);
                    string extension = Path.GetExtension(Image.FileName);
                    pic = pic+ DateTime.Now.ToString("yymmssfff")+extension;
                    d.Image = "~/Image/" + pic;
                    pic  = Path.Combine(Server.MapPath("~/Image/"), pic);
                    Image.SaveAs(pic);
                    d.Notes = Form["Notes"];
                    d.Date = DateTime.Now.ToString("dd/MM/yyyy");

                    dRepo.Update(d);
                    return RedirectToAction("Index");
                }
            }
            else 
            {
                return View("Edit");
            }
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(dRepo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            dRepo.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var diary = dRepo.GetAll().SingleOrDefault(x => x.ID == id);
            return View(diary);
        }


    }
}