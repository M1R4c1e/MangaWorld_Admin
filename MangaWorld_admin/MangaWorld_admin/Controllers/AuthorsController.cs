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
    public class AuthorsController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: Authors
        public ActionResult Index()
        {
            if (SessionCheck.onSession())
            {
                return View(db.Authors.ToList());
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Authors/Details/5
        public ActionResult Details(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Author author = db.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            if (SessionCheck.onSession())
            {
                return View();
            }
            return RedirectToAction("");
            
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorId,AuthorName,Biography,Socials")] Author author)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(author);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Author author = db.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);
            }
            return RedirectToAction("Login", "Home");

        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorId,AuthorName,Biography,Socials")] Author author)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(author).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(author);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Author author = db.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);
            }
            return RedirectToAction("Login", "Home");

        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (SessionCheck.onSession())
            {
                Author author = db.Authors.Find(id);
                db.Authors.Remove(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Home");

        }

        public JsonResult Filter(String name)
        {
            var authors = db.Authors.Include(a => a.Mangas);
            if (!name.Equals(""))
            {
                authors = authors.Where(a => a.AuthorName.Contains(name));
            }
            List<string> list = new List<string>();
            foreach (var a in authors.ToList())
            {
                list.Add(a.ToString());
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
