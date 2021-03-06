﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JassWeather.Models;
using System.Threading;
using System.Threading.Tasks;

namespace JassWeather.Controllers
{
    [Authorize]
    public class BuilderController : JassController
    {
        private JassWeatherContext db = new JassWeatherContext();
        //
        // GET: /Builder/


        public ActionResult ProcessBuilderAndUploadAllCleanAsync(int id = 0)
        {
            string result = "Task has been schedule";

            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            jassbuilder.JassGrid = db.JassGrids.Find(jassbuilder.JassGridID);
            jassbuilder.JassVariable = db.JassVariables.Find(jassbuilder.JassVariableID);
            jassbuilder.APIRequest= db.APIRequests.Find(jassbuilder.APIRequestId);
   
            Task task = Task.Run(() =>
            {
                 result = apiCaller.processBuilderAll(jassbuilder, true, true);
            });
            JassController.task = task;
            return View(jassbuilder);
        }

        public ActionResult QueryBuilderStatus(int id = 0)
        {
            string result = "QueryBuilderStatus";
            if (JassController.task == null)
            {
                result = "No Active Tasks Found";
            }
            else
            {

                Task task = JassController.task;
                result = task.Status.ToString();

            }

            ViewBag.Message = result;
            return View();
        }




        public ActionResult Index()
        {
            var jassbuilders = db.JassBuilders.Include(j => j.JassVariable).Include(j => j.JassGrid).Include(j => j.APIRequest);
            return View(jassbuilders.ToList());
        }

        //
        // GET: /Builder/Details/5

        public ActionResult Details(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            return View(jassbuilder);
        }

        public ActionResult ProcessBuilder(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            JassBuilderLog builderLog = apiCaller.createBuilderLog(jassbuilder, "ProcessBuilder_Start", "Test", "Start", DateTime.Now - DateTime.Now, true);
            int year = (jassbuilder.year == null) ? 0 : (int)jassbuilder.year;
            int month = (jassbuilder.month == null) ? 0 : (int)jassbuilder.month;
            int weeky = (jassbuilder.weeky == null) ? 0 : (int)jassbuilder.weeky;
            int day = (jassbuilder.day == null) ? 0 : (int)jassbuilder.day;
            var result = apiCaller.processBuilder(jassbuilder, year, month, weeky, day, false, builderLog);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }

        public ActionResult TestBuilder(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            var result = apiCaller.testBuilderOnDisk(jassbuilder, false);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }

        public ActionResult ProcessBuilderAndUpload(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);

            JassBuilderLog builderLog = apiCaller.createBuilderLog(jassbuilder, "ProcessBuilderAndUpload_Start", "Test", "Start", DateTime.Now - DateTime.Now, true);
            int year = (jassbuilder.year == null) ? 0 : (int)jassbuilder.year;
            int month = (jassbuilder.month == null) ? 0 : (int)jassbuilder.month;
            int weeky = (jassbuilder.weeky == null) ? 0 : (int)jassbuilder.weeky;
            int day = (jassbuilder.day == null) ? 0 : (int)jassbuilder.day;

            var result = apiCaller.processBuilder(jassbuilder, year, month, weeky, day, true, builderLog);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }

        public ActionResult ProcessBuilderAndUploadAllClean(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            var result = apiCaller.processBuilderAll(jassbuilder, true, true);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }

        public ActionResult ProcessBuilderAndUploadAll(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            var result = apiCaller.processBuilderAll(jassbuilder, true, false);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }
        public ActionResult ProcessBuilderAllLocally(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            var result = apiCaller.processBuilderAll(jassbuilder, false, false);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }

        public ActionResult CheckBuilderOnDisk(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            var result = apiCaller.checkBuilderOnDisk(jassbuilder);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }

        public ActionResult CleanBuilderOnDisk(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            var result = apiCaller.cleanBuilderOnDisk(jassbuilder);

            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = result;

            return View(jassbuilder);
        }


        //
        // GET: /Builder/Create

        public ActionResult Create()
        {
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name");
            ViewBag.JassGridID = new SelectList(db.JassGrids, "JassGridID", "Name");
            ViewBag.APIRequestId = new SelectList(db.APIRequests, "Id", "url");
            return View();
        }

        //
        // POST: /Builder/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JassBuilder jassbuilder)
        {
            if (ModelState.IsValid)
            {
                db.JassBuilders.Add(jassbuilder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassbuilder.JassVariableID);
            ViewBag.JassGridID = new SelectList(db.JassGrids, "JassGridID", "Name", jassbuilder.JassGridID);
            ViewBag.APIRequestId = new SelectList(db.APIRequests, "Id", "url", jassbuilder.APIRequestId);
            return View(jassbuilder);
        }

        //
        // GET: /Builder/Edit/5

        public ActionResult Edit(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassbuilder.JassVariableID);
            ViewBag.JassGridID = new SelectList(db.JassGrids, "JassGridID", "Name", jassbuilder.JassGridID);
            ViewBag.APIRequestId = new SelectList(db.APIRequests, "Id", "url", jassbuilder.APIRequestId);
            return View(jassbuilder);
        }

        //
        // POST: /Builder/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JassBuilder jassbuilder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jassbuilder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassbuilder.JassVariableID);
            ViewBag.JassGridID = new SelectList(db.JassGrids, "JassGridID", "Name", jassbuilder.JassGridID);
            ViewBag.APIRequestId = new SelectList(db.APIRequests, "Id", "url", jassbuilder.APIRequestId);
            return View(jassbuilder);
        }

        public ActionResult EditAndRun(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassbuilder.JassVariableID);
            ViewBag.JassGridID = new SelectList(db.JassGrids, "JassGridID", "Name", jassbuilder.JassGridID);
            ViewBag.APIRequestId = new SelectList(db.APIRequests, "Id", "url", jassbuilder.APIRequestId);
            return View(jassbuilder);
        }

        //
        // POST: /Builder/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAndRun(JassBuilder jassbuilder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jassbuilder).State = EntityState.Modified;
                db.SaveChanges();
                var jassbuilder2 = db.JassBuilders.Find(jassbuilder.JassBuilderID);
                jassbuilder2.JassVariable = db.JassVariables.Find(jassbuilder2.JassVariableID);
                jassbuilder2.JassGrid = db.JassGrids.Find(jassbuilder2.JassGridID);
                jassbuilder2.APIRequest = db.APIRequests.Find(jassbuilder2.APIRequestId);
                var result = apiCaller.processBuilderAll(jassbuilder2, true, true);
                return RedirectToAction("Index");
            }
            ViewBag.JassVariableID = new SelectList(db.JassVariables, "JassVariableID", "Name", jassbuilder.JassVariableID);
            ViewBag.JassGridID = new SelectList(db.JassGrids, "JassGridID", "Name", jassbuilder.JassGridID);
            ViewBag.APIRequestId = new SelectList(db.APIRequests, "Id", "url", jassbuilder.APIRequestId);
            return View(jassbuilder);
        }



        //
        // GET: /Builder/Delete/5

        public ActionResult Delete(int id = 0)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            if (jassbuilder == null)
            {
                return HttpNotFound();
            }
            return View(jassbuilder);
        }

        //
        // POST: /Builder/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JassBuilder jassbuilder = db.JassBuilders.Find(id);
            db.JassBuilders.Remove(jassbuilder);
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