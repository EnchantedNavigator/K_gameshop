using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PepegaRequiem.Models;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;
using System.Security.Principal;
using System.Web.Security;

namespace PepegaRequiem.Controllers
{

    public class HomeController : Controller
    {
        
        private PepegaContext db = new PepegaContext();
        private int? currentPage;

        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return RedirectToAction("IndexP");
        }

            public ActionResult IndexP(int? page)
        {
           // if (User.Identity.IsAuthenticated)
          //  {
           //   ViewBag.UserName =  User.Identity.Name;
          //  }
          //  else
           // {
          //      ViewBag.UserName = "Not Authorized"; 
          //  }

            SelectList categories = new SelectList(db.Categories, "Id", "Name");
            SelectList developers = new SelectList(db.Developers, "Id", "Name");
            ViewBag.Categories = categories;
            ViewBag.Developers = developers;
            var games = db.Games.Include(p => p.Category)
          .Include(p => p.Developer);
            ViewBag.Games = games;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            games = games.OrderBy(game =>game.GameId);
            if (page != null)
            {
                currentPage = page;
            }
            return View("Index",games.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult IndexF(int? developer, int? category)
        {
            IQueryable<Game> games = db.Games.Include(p => p.Developer)
                .Include(p => p.Category);
            if (developer !=null && developer !=0)
            {
                games = games.Where(p => p.DeveloperID == developer);
            }
            if (category != null && category != 0)
            {
                games = games.Where(p => p.CategoryID == category);
            }
            List<Developer> developers = db.Developers.ToList();
            developers.Insert(0, new Developer { Name = "All", Id = 0 });
            List<Category> categories = db.Categories.ToList();
            categories.Insert(0, new Category { Name = "All", Id = 0 });
            ViewBag.Games = games;
            ViewBag.Developers = new SelectList(developers, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            int pageSize = 3;
            int pageNumber = (currentPage ?? 1);
            games = games.OrderBy(game => game.GameId);
            return View("Index",games.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult DetailsGames(int? id)
        {

            IQueryable<Game> games = db.Games.Include(p => p.Developer)
                  .Include(p => p.Category);
            Game a = db.Games.Find(id);
            ViewBag.Category = a.Category;
            ViewBag.Developer = a.Developer;
            ViewBag.Image = a.Image;
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpGet]
        public ActionResult DetailsCategories(int? id)
        {
            Category a = db.Categories.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            ViewBag.Image = a.Image;
            return View(a);
        }
        [HttpGet]
        public ActionResult DetailsDevelopers(int? id)
        {
            Developer a = db.Developers.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            ViewBag.Image = a.Image;
            return View(a);
        }
        [ChildActionOnly]
        public ActionResult UserLogin()
        {

            ViewBag.UserName = User.Identity.Name;
            return View();

        }
        [ChildActionOnly]
        public ActionResult Card()
        {
            return View();
        }
        /*
        [HttpGet]
        public ActionResult Card(int? id)
        {
            Game a = db.Games.Find(id);
            return PartialView(a);
        }*/
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.DateTime = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return $"Ok {purchase.User} , your game is ready";
        }

       
       
    }

}