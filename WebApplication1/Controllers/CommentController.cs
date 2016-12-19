using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        Entities e = new Entities();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        [Authorize(Roles = "reader")]
        public ActionResult Create(int id_book)
        {
            var b = e.Books.Find(id_book);
            ViewBag.book = b;
            Logger.Log.Info("User" + User.Identity.GetUserId() + " wants to create a comment for book " + id_book);
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        [Authorize(Roles = "reader")]
        public ActionResult Create(int id_book, Comment comment)
        {
            try
            {                
                comment.Id_author = User.Identity.GetUserId();
                comment.Id_book = id_book;
                comment.Status = "Составлен";
                e.Comments.Add(comment);
                e.SaveChanges();
                Logger.Log.Info("User" + User.Identity.GetUserId() + " created a comment for book " + id_book);
                return RedirectToAction("../Book/Reading", new {id = id_book});
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            Logger.Log.Info("Admin" + User.Identity.GetUserId() + " wants to edit comment " + id);
            return View(e.Comments.Find(id));
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, Comment comm)
        {
            try
            {
                e.Comments.Find(id).Status = comm.Status;
                e.SaveChanges();
                Logger.Log.Info("Admin" + User.Identity.GetUserId() + " changed status of comment " + id + " with "+comm.Status);
                return RedirectToAction("../Manage/IndexManager");
            }
            catch (Exception e)
            {
                Logger.Log.Info(e.Message);
                return View();
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
