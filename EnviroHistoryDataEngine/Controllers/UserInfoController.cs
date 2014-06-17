using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JassWeather.Models;

namespace JassWeather.Controllers
{
    public class UserInfoController : Controller
    {
        private JassWeatherContext db = new JassWeatherContext();

        //
        // GET: /UserInfo/

        public ActionResult Index()
        {
            var jassuserinfoes = db.JassUserInfoes.Include(j => j.JassVariableGroup).Include(j => j.JassLatLonGroup);
            return View(jassuserinfoes.ToList());
        }

        //
        // GET: /UserInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            JassUserInfo jassuserinfo = db.JassUserInfoes.Find(id);
            if (jassuserinfo == null)
            {
                return HttpNotFound();
            }
            return View(jassuserinfo);
        }

        //
        // GET: /UserInfo/Create

        public ActionResult Create()
        {
            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name");
            ViewBag.JassLatLonGroupID = new SelectList(db.JassLatLonGroups, "JassLatLonGroupID", "Name");
            return View();
        }

        //
        // POST: /UserInfo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JassUserInfo jassuserinfo)
        {
            if (ModelState.IsValid)
            {
                db.JassUserInfoes.Add(jassuserinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name", jassuserinfo.JassVariableGroupID);
            ViewBag.JassLatLonGroupID = new SelectList(db.JassLatLonGroups, "JassLatLonGroupID", "Name", jassuserinfo.JassLatLonGroupID);
            return View(jassuserinfo);
        }

        //
        // GET: /UserInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            JassUserInfo jassuserinfo = db.JassUserInfoes.Find(id);
            if (jassuserinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name", jassuserinfo.JassVariableGroupID);
            ViewBag.JassLatLonGroupID = new SelectList(db.JassLatLonGroups, "JassLatLonGroupID", "Name", jassuserinfo.JassLatLonGroupID);
            return View(jassuserinfo);
        }

        //
        // POST: /UserInfo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JassUserInfo jassuserinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jassuserinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name", jassuserinfo.JassVariableGroupID);
            ViewBag.JassLatLonGroupID = new SelectList(db.JassLatLonGroups, "JassLatLonGroupID", "Name", jassuserinfo.JassLatLonGroupID);
            return View(jassuserinfo);
        }

        //
        // GET: /UserInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            JassUserInfo jassuserinfo = db.JassUserInfoes.Find(id);
            if (jassuserinfo == null)
            {
                return HttpNotFound();
            }
            return View(jassuserinfo);
        }

        //
        // POST: /UserInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JassUserInfo jassuserinfo = db.JassUserInfoes.Find(id);
            db.JassUserInfoes.Remove(jassuserinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}