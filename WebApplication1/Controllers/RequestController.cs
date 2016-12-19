using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RequestController : Controller
    {
        Entities e = new Entities();

        // GET: Request
        [Authorize(Roles = "reader")]
        public ActionResult MyRequests()
        {
            string userid = User.Identity.GetUserId();
            var list = e.Requests.Where(p => p.Id_sender == userid);
            Logger.Log.Info("User" + User.Identity.GetUserId() + " wants to watch his requests");
            return View(list);
        }

        //GET: Request - редактирование статусов из-под менеджера
        [Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            Logger.Log.Info("Manager" + User.Identity.GetUserId() + " wants to edit requests");
            return View(e.Requests);
        }

        // GET: Request/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Request/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Create(Book book)
        {
            try
            {
                int max_id = e.Books.Max(p => p.Id);
                book.Id = max_id+1;
                e.Books.Add(book);
                Request req = new Models.Request();
                req.Id = e.Requests.Max(p => p.Id) + 1;
                req.Id_book = book.Id;
                req.Id_sender = User.Identity.GetUserId();
                req.Status = "Составлена";
                e.Requests.Add(req);
                e.SaveChanges();
                Logger.Log.Info("Manager" + User.Identity.GetUserId() + " created a book " + book.Id);
                return RedirectToAction("MyRequests");

            }
            catch (Exception e)
            {
                Logger.Log.Info(e.Message);
                return RedirectToAction("Error", e);
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        // GET: Request/Edit/5
        [Authorize(Roles = "manager")]
        public ActionResult Edit(int id)
        {
            Logger.Log.Info("Manager" + User.Identity.GetUserId() + " wants to edit request " + id);
            return View(e.Requests.Find(id));
        }

        // POST: Request/Edit/5
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Edit(int id, Request req)
        {
            try
            {               
                e.Requests.Find(id).Status = req.Status;
                e.SaveChanges();
                Logger.Log.Info("Manager" + User.Identity.GetUserId() + " changed the status of request " + id + " with "+req.Status);
                return RedirectToAction("../Manage/IndexManager");
            }
            catch (Exception e)
            {
                Logger.Log.Info(e.Message);
                return View();
            }
        }

        // GET: Request/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Request/Delete/5
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
