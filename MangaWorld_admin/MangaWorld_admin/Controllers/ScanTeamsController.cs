using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MangaWorld_admin.Models;

namespace MangaWorld_admin.Controllers
{
    public class ScanTeamsController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: ScanTeams
        public ActionResult Index()
        {
            if (SessionCheck.onSession())
            {
                var scanTeams = db.ScanTeams.Include(s => s.Language);
                return View(scanTeams.ToList());
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: ScanTeams/Details/5
        public ActionResult Details(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ScanTeam scanTeam = db.ScanTeams.Find(id);
                if (scanTeam == null)
                {
                    return HttpNotFound();
                }
                return View(scanTeam);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: ScanTeams/Create
        public ActionResult Create()
        {
            if (SessionCheck.onSession())
            {
                ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName");
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: ScanTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScanTeamId,TeamName,TeamDescription,TeamSocials,LanguageId,Deleted")] ScanTeam scanTeam)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.ScanTeams.Add(scanTeam);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", scanTeam.LanguageId);
                return View(scanTeam);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: ScanTeams/Edit/5
        public ActionResult Edit(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ScanTeam scanTeam = db.ScanTeams.Find(id);
                if (scanTeam == null)
                {
                    return HttpNotFound();
                }
                ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", scanTeam.LanguageId);
                return View(scanTeam);
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: ScanTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScanTeamId,TeamName,TeamDescription,TeamSocials,LanguageId,Deleted")] ScanTeam scanTeam)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(scanTeam).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", scanTeam.LanguageId);
                return View(scanTeam);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: ScanTeams/Delete/5
        public ActionResult Delete(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ScanTeam scanTeam = db.ScanTeams.Find(id);
                if (scanTeam == null)
                {
                    return HttpNotFound();
                }
                return View(scanTeam);
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: ScanTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (SessionCheck.onSession())
            {
                ScanTeam scanTeam = db.ScanTeams.Find(id);
                db.ScanTeams.Remove(scanTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Home");
        }
        public JsonResult Filter(String name)
        {
            var teams = db.ScanTeams.Include(s => s.Language);
            if (!name.Equals(""))
            {
                teams = teams.Where(s => s.TeamName.Contains(name));
            }
            List<string> list = new List<string>();
            foreach (var t in teams.ToList())
            {
                list.Add(t.ToString());
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
