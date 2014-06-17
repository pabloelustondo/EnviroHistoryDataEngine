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
    public class VariableGroupController : Controller
    {
        private JassWeatherContext db = new JassWeatherContext();

        //
        // GET: /VariableGroup/

        public ActionResult Index()
        {
            return View(db.JassVariableGroups.ToList());
        }

        //
        // GET: /VariableGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            JassVariableGroup jassvariablegroup = db.JassVariableGroups.Find(id);
            if (jassvariablegroup == null)
            {
                return HttpNotFound();
            }
            return View(jassvariablegroup);
        }

        //
        // GET: /VariableGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VariableGroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JassVariableGroup jassvariablegroup)
        {
            if (ModelState.IsValid)
            {
                db.JassVariableGroups.Add(jassvariablegroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jassvariablegroup);
        }

        //
        // GET: /VariableGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            JassVariableGroup jassvariablegroup = db.JassVariableGroups.Find(id);
            if (jassvariablegroup == null)
            {
                return HttpNotFound();
            }
            return View(jassvariablegroup);
        }

        //
        // POST: /VariableGroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JassVariableGroup jassvariablegroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jassvariablegroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jassvariablegroup);
        }

        //
        // GET: /VariableGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            JassVariableGroup jassvariablegroup = db.JassVariableGroups.Find(id);
            if (jassvariablegroup == null)
            {
                return HttpNotFound();
            }
            return View(jassvariablegroup);
        }

        //
        // POST: /VariableGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JassVariableGroup jassvariablegroup = db.JassVariableGroups.Find(id);
            db.JassVariableGroups.Remove(jassvariablegroup);
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