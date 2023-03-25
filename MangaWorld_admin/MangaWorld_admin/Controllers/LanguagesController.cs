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
    public class LanguagesController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: Languages
        public ActionResult Index()
        {
            if (SessionCheck.onSession())
            {
                return View(db.Languages.ToList());
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: Languages/Details/5
        public ActionResult Details(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Language language = db.Languages.Find(id);
                if (language == null)
                {
                    return HttpNotFound();
                }
                return View(language);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            if (SessionCheck.onSession())
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LanguageId,LanguageName,CountryFlag")] Language language)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Languages.Add(language);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(language);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Language language = db.Languages.Find(id);
                if (language == null)
                {
                    return HttpNotFound();
                }
                return View(language);
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LanguageId,LanguageName,CountryFlag")] Language language)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(language).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(language);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: Languages/Delete/5
        public ActionResult Delete(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Language language = db.Languages.Find(id);
                if (language == null)
                {
                    return HttpNotFound();
                }
                return View(language);
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (SessionCheck.onSession())
            {
                Language language = db.Languages.Find(id);
                db.Languages.Remove(language);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Home");
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
