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
    public class CommentsController : Controller
    {
        private DBcontext db = new DBcontext();

        // GET: Comments
        public ActionResult Index(string id, int? page)
        {
            if (SessionCheck.onSession())
            {
                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                var comments = db.Comments.Include(c => c.Manga).Include(c => c.User).Where(c => c.MangaId.Equals(id)).OrderByDescending(c => c.CommentDate);
                ViewBag.MangaTitle = db.Mangas.Find(id).Title;
                return View(comments.ToPagedList(pageIndex, pageSize));
            }
            return RedirectToAction("Login", "Home");
        }


        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (SessionCheck.onSession())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                return View(comment);
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (SessionCheck.onSession())
            {
                Comment comment = db.Comments.Find(id);
                db.Comments.Remove(comment);
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
