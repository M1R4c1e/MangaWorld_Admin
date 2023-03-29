using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MangaWorld_admin.Models;
using PagedList;

namespace MangaWorld_admin.Controllers
{
    public class ChaptersController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: Chapters
        public ActionResult Index(string id, int? page)
        {
            if (SessionCheck.onSession())
            {
                int pageSize = 5;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                if (id != null)
                {
                    var chapters = db.Chapters.Include(c => c.ChapterType).Include(c => c.Manga).Include(c => c.ScanTeam).Where(c => c.MangaId.Equals(id)).OrderBy(c => c.ChapterOrder);
                    return View(chapters.ToPagedList(pageIndex, pageSize));
                }
                ViewBag.NoBack = true;
                return View(db.Chapters.Include(c => c.ChapterType).Include(c => c.Manga).Include(c => c.ScanTeam).ToPagedList(pageIndex, pageSize));
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Chapters/Details/5
        public ActionResult Details(string id, float chapOrder)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var chapters = db.Chapters.Where(c => c.MangaId.Equals(id) && c.ChapterOrder == chapOrder).ToList();
                Chapter chapter = chapters.ElementAt(0);
                if (chapter == null)
                {
                    return HttpNotFound();
                }
                return View(chapter);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Chapters/Create
        public ActionResult Create()
        {
            if (SessionCheck.onSession())
            {
                ViewBag.ChapterTypeId = new SelectList(db.ChapterTypes, "ChapterTypeId", "ChapterTypeName");
                ViewBag.MangaId = new SelectList(db.Mangas, "MangaId", "Title");
                ViewBag.ScanTeamId = new SelectList(db.ScanTeams, "ScanTeamId", "TeamName");
                return View();
            }
            return RedirectToAction("Login", "Home");

        }

        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MangaId,ChapterOrder,ChapterTitle,ChapterTypeId,PageNum,UploadDate,ScanTeamId,IsPublished,Deleted,ChapterPath")] Chapter chapter)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Chapters.Add(chapter);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ChapterTypeId = new SelectList(db.ChapterTypes, "ChapterTypeId", "ChapterTypeName", chapter.ChapterTypeId);
                ViewBag.MangaId = new SelectList(db.Mangas, "MangaId", "Title", chapter.MangaId);
                ViewBag.ScanTeamId = new SelectList(db.ScanTeams, "ScanTeamId", "TeamName", chapter.ScanTeamId);
                return View(chapter);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Chapters/Edit/5
        public ActionResult Edit(string id, float chapOrder)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var chapters = db.Chapters.Where(c => c.MangaId.Equals(id) && c.ChapterOrder == chapOrder).ToList();
                Chapter chapter = chapters.ElementAt(0);
                if (chapter == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ChapterTypeId = new SelectList(db.ChapterTypes, "ChapterTypeId", "ChapterTypeName", chapter.ChapterTypeId);
                ViewBag.MangaId = new SelectList(db.Mangas, "MangaId", "Title", chapter.MangaId);
                ViewBag.ScanTeamId = new SelectList(db.ScanTeams, "ScanTeamId", "TeamName", chapter.ScanTeamId);
                return View(chapter);
            }
            return RedirectToAction("Login", "Home");

        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MangaId,ChapterOrder,ChapterTitle,ChapterTypeId,PageNum,UploadDate,ScanTeamId,IsPublished,Deleted,ChapterPath")] Chapter chapter)
        {
            if (SessionCheck.onSession())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(chapter).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ChapterTypeId = new SelectList(db.ChapterTypes, "ChapterTypeId", "ChapterTypeName", chapter.ChapterTypeId);
                ViewBag.MangaId = new SelectList(db.Mangas, "MangaId", "Title", chapter.MangaId);
                ViewBag.ScanTeamId = new SelectList(db.ScanTeams, "ScanTeamId", "TeamName", chapter.ScanTeamId);
                return View(chapter);
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Chapters/Delete/5
        public ActionResult Delete(string id, float chapOrder)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var chapters = db.Chapters.Where(c => c.MangaId.Equals(id) && c.ChapterOrder == chapOrder).ToList();
                Chapter chapter = chapters.ElementAt(0);
                if (chapter == null)
                {
                    return HttpNotFound();
                }
                return View(chapter);
            }
            return RedirectToAction("Login", "Home");

        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (SessionCheck.onSession())
            {
                Chapter chapter = db.Chapters.Find(id);
                db.Chapters.Remove(chapter);
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
