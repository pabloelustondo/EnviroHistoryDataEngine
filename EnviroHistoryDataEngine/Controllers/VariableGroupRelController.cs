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
    public class VariableGroupRelController : Controller
    {
        private JassWeatherContext db = new JassWeatherContext();

        //
        // GET: /VariableGroupRel/

        public ActionResult Index()
        {
            var jassvariablegrouprels = db.JassVariableGroupRels.Include(j => j.JassVariableGroup).Include(j => j.JassVariable);
            return View(jassvariablegrouprels.ToList());
        }

        //
        // GET: /VariableGroupRel/Details/5

        public ActionResult Details(int id = 0)
        {
            JassVariableGroupRel jassvariablegrouprel = db.JassVariableGroupRels.Find(id);
            if (jassvariablegrouprel == null)
            {
                return HttpNotFound();
            }
            return View(jassvariablegrouprel);
        }

        //
        // GET: /VariableGroupRel/Create

        public ActionResult Create()
        {
            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name");
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name");
            return View();
        }

        //
        // POST: /VariableGroupRel/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JassVariableGroupRel jassvariablegrouprel)
        {
            if (ModelState.IsValid)
            {
                db.JassVariableGroupRels.Add(jassvariablegrouprel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name", jassvariablegrouprel.JassVariableGroupID);
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassvariablegrouprel.JassVariableID);
            return View(jassvariablegrouprel);
        }

        //
        // GET: /VariableGroupRel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            JassVariableGroupRel jassvariablegrouprel = db.JassVariableGroupRels.Find(id);
            if (jassvariablegrouprel == null)
            {
                return HttpNotFound();
            }
            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name", jassvariablegrouprel.JassVariableGroupID);
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassvariablegrouprel.JassVariableID);
            return View(jassvariablegrouprel);
        }

        //
        // POST: /VariableGroupRel/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JassVariableGroupRel jassvariablegrouprel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jassvariablegrouprel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JassVariableGroupID = new SelectList(db.JassVariableGroups, "JassVariableGroupID", "Name", jassvariablegrouprel.JassVariableGroupID);
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassvariablegrouprel.JassVariableID);
            return View(jassvariablegrouprel);
        }

        //
        // GET: /VariableGroupRel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            JassVariableGroupRel jassvariablegrouprel = db.JassVariableGroupRels.Find(id);
            if (jassvariablegrouprel == null)
            {
                return HttpNotFound();
            }
            return View(jassvariablegrouprel);
        }

        //
        // POST: /VariableGroupRel/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JassVariableGroupRel jassvariablegrouprel = db.JassVariableGroupRels.Find(id);
            db.JassVariableGroupRels.Remove(jassvariablegrouprel);
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