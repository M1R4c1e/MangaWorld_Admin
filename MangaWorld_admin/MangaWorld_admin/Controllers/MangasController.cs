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
    public class MangasController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: Mangas
        public ActionResult Index()
        {
            var mangas = db.Mangas.Include(m => m.Author).Include(m => m.Language).Include(m => m.Status1);
            var genres = db.Genres.ToList();
            var authors = db.Authors.ToList();
            var langs = db.Languages.ToList();
            ViewBag.Genres = genres;
            ViewBag.Authors = authors;
            ViewBag.Langs = langs;
            return View(mangas.ToList());
        }

        // GET: Mangas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            return View(manga);
        }

        // GET: Mangas/Create
        public ActionResult Create()
        {
            ViewBag.Genres = db.Genres.ToList();
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName");
            ViewBag.Status = new SelectList(db.Status, "StatusId", "StatusName");
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MangaId,Title,AltTitle,MangaPath,Summary,AuthorId,Genres,LanguageId,Status,ReleasedYear,IsPublished")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                manga.Deleted = false;
                db.Mangas.Add(manga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", manga.AuthorId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", manga.LanguageId);
            ViewBag.Status = new SelectList(db.Status, "StatusId", "StatusName", manga.Status);
            return View(manga);
        }

        // GET: Mangas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", manga.AuthorId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", manga.LanguageId);
            ViewBag.Status = new SelectList(db.Status, "StatusId", "StatusName", manga.Status);
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MangaId,Title,AltTitle,MangaPath,Summary,AuthorId,Genres,LanguageId,Status,ReleasedYear,IsPublished,Deleted")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", manga.AuthorId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", manga.LanguageId);
            ViewBag.Status = new SelectList(db.Status, "StatusId", "StatusName", manga.Status);
            return View(manga);
        }

        // GET: Mangas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manga manga = db.Mangas.Find(id);
            if (manga == null)
            {
                return HttpNotFound();
            }
            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Manga manga = db.Mangas.Find(id);
            db.Mangas.Remove(manga);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult Filter(String name, String genres, String author, String language)
        {
            var mangas = db.Mangas.Include(m => m.Author).Include(m => m.Language).Include(m => m.Status1);
            List<string> list = new List<string>();
            if (!name.Equals(""))
            {
                mangas = mangas.Where(m => m.Title.Contains(name) || m.AltTitle.Contains(name));
            }
            if (!author.Equals(""))
            {
                mangas = mangas.Where(m => m.Author.AuthorName.Equals(author));
            }
            if (!language.Equals(""))
            {
                mangas = mangas.Where(m => m.Language.LanguageName.Equals(language));
            }
            if (!genres.Equals(""))
            {
                List<Manga> temp = new List<Manga>();
                String[] listGen = genres.Split('*');
                for (int i = 0; i < mangas.ToList().Count; i++)
                {
                    bool f = true;
                    for (int j = 0; j < listGen.Length - 1; j++)
                    {
                        if (!mangas.ToList().ElementAt(i).Genres.Contains(listGen[j]))
                        {
                            f = false;
                            break;
                        }
                    }
                    if (f) { temp.Add(mangas.ToList().ElementAt(i)); }
                }
                foreach (var m in temp)
                {
                    list.Add(m.ToString());
                }
            }
            else
            {
                foreach (var m in mangas.ToList())
                {
                    list.Add(m.ToString());
                }
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
