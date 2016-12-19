using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        Entities e = new Entities();

        // GET: Book
        [Authorize(Roles = "reader")]
        public ActionResult MyBooks()
        {
            string userid = User.Identity.GetUserId();
            reader r = e.readers.Find(userid);
            var list = r.Books;
            Logger.Log.Info("MyBooks");
            return View(list);
        }

        [Authorize(Roles = "reader")]
        public ActionResult Reading(int id)
        {
            Book read = e.Books.Find(id);
            ViewData["Comm"] = e.Comments.Where(p => (p.Id_book == id) && (p.Status == "Опубликован")).ToList();
            Logger.Log.Info("Reading book " + id);
            return View(read);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        [Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Create(Book book)
        {
            try
            {
                e.Books.Add(book);
                Logger.Log.Info("Sucsess create book "+ book.Id);
                return RedirectToAction("MyBooks");
            }
            catch (Exception e)
            {
                Logger.Log.Info(e.Message);
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        [Authorize(Roles = "reader")]
        public ActionResult Delete(int id)
        {

            Book b = e.Books.Find(id);
            Logger.Log.Info("User"+User.Identity.GetUserId()+" wants to delete book "+id+" from Favourites");
            return View(b);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [Authorize(Roles = "reader")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                reader r = e.readers.Find(userid);
                Book b = r.Books.First(p => p.Id == id);
                r.Books.Remove(b);
                e.SaveChanges();
                Logger.Log.Info("User" + userid + " deleted book " + id + " from Favourites");
                return RedirectToAction("MyBooks");
            }
            catch
            {
                return View();
            }
        }
    }
}
