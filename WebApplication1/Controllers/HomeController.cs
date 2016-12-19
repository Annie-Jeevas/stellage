using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Entities e = new Entities();


        //первый вход - показываем все действительные книги
        public ActionResult Index()
        {

            Logger.Log.Info("Вход в Home/Index");
            ViewData["allBooks"] = e.Books.Where(p => p.File != null).ToList();
            return View();
        }


        // GET: Book/Create
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                int max_id = e.Books.Max(p => p.Id);
                book.Id = max_id + 1;
                e.Books.Add(book);
                e.SaveChanges();
                Logger.Log.Info("Создание книги: " + book.Id);
                return RedirectToAction("MoreBooks");
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message, e);
                return View();
            }
        }

        //переход к сведеньям о программе
        public ActionResult About()
        {
            Logger.Log.Info("About");
            ViewBag.Message = "Your application description page.";
            return View();
        }

        //контакты
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            Logger.Log.Info("Contact");
            return View();
        }


        // GET: Home/Details/5 - не занято
        public ActionResult Details(int id)
        {
            return View("");
        }


        //ссылка внизу книг - переход к каталогу

        public ActionResult MoreBooks()
        {
            Logger.Log.Info("MoreBooks");
            return View(e.Books.Where(p => p.File != null));
        }



        // GET: Home/Add - добавление книги из каталога в избранное
        [Authorize(Roles = "reader")]
        public ActionResult AddToMyBooks(int id)
        {
            string userid = User.Identity.GetUserId();
            reader r = e.readers.Find(userid);
            Book b = e.Books.Find(id);
            r.Books.Add(b);
            e.SaveChanges();
            Logger.Log.Info("User " + userid + " adds the book " + b.Id + "to Favourites.");
            return RedirectToAction("../Book/MyBooks");
        }




    }
}