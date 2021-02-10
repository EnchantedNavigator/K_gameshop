using System;
using System.IO;
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
    public class AdminController : Controller
    {

        private PepegaContext db = new PepegaContext();
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Games()
        {
            IQueryable<Game> games = db.Games.Include(p => p.Developer)
                  .Include(p => p.Category);
            return View(games);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Categories()
        {
            IQueryable<Category> categories = db.Categories;
            return View(categories);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Developers()
        {
            IQueryable<Developer> developers = db.Developers;
            return View(developers);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddGame()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "Name");
            SelectList developers = new SelectList(db.Developers, "Id", "Name");
            ViewBag.Categories = categories;
            ViewBag.Developers = developers;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddGame(Game game, HttpPostedFileBase productImg, int? developer, int? category)
        {
           // if (ModelState.IsValid)
           // {
                if (productImg != null)
                {
                    var fileName = Path.GetFileName(productImg.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Games"));

                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    productImg.SaveAs(pathToSave);
                    game.Image = fileName;
                }
                    game.CategoryID = category;
                    game.DeveloperID = developer;
                
                db.Games.Add(game);
                db.SaveChanges();
         //   }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddCategory(Category category, HttpPostedFileBase productImg)
        {
            if (ModelState.IsValid)
            {
                if (productImg != null)
                {
                    var fileName = Path.GetFileName(productImg.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Categories"));

                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    productImg.SaveAs(pathToSave);
                    category.Image = fileName;
                }
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddDeveloper()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddDeveloper(Developer developer, HttpPostedFileBase productImg)
        {
            if (ModelState.IsValid)
            {
                if (productImg != null)
                {
                    var fileName = Path.GetFileName(productImg.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Developers"));

                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    productImg.SaveAs(pathToSave);
                    developer.Image = fileName;
                }
                db.Developers.Add(developer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCategory(int? id)
        {

            Category a = db.Categories.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }
        [HttpPost, ActionName("DeleteCategory")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCategoryConfirmed(int? id)
        {

            Category a = db.Categories.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            if (a.Image!=null)
            {
                var fileName = Path.GetFileName(a.Image);
                var directoryToDelete = Server.MapPath(Url.Content("~/Pictures/Categories"));
                var pathToDelete = Path.Combine(directoryToDelete, fileName);
                System.IO.File.Delete(pathToDelete);
            }
            db.Categories.Remove(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public ActionResult DeleteDeveloper(int? id)
        {

            Developer a = db.Developers.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }

            return View(a);
        }
        [HttpPost, ActionName("DeleteDeveloper")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteDeveloperConfirmed(int? id)
        {

           Developer a = db.Developers.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            if (a.Image != null)
            {
                var fileName = Path.GetFileName(a.Image);
                var directoryToDelete = Server.MapPath(Url.Content("~/Pictures/Developers"));
                var pathToDelete = Path.Combine(directoryToDelete, fileName);
                System.IO.File.Delete(pathToDelete);
            }
            db.Developers.Remove(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteGame(int? id)
        {

            Game a = db.Games.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }
        [HttpPost, ActionName("DeleteGame")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteGameConfirmed(int? id)
        {

            Game a = db.Games.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            if (a.Image != null)
            {
                var fileName = Path.GetFileName(a.Image);
                var directoryToDelete = Server.MapPath(Url.Content("~/Pictures/Games"));
                var pathToDelete = Path.Combine(directoryToDelete, fileName);
                System.IO.File.Delete(pathToDelete);
            }
            db.Games.Remove(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditGame(int? id)
        {
            SelectList categories = new SelectList(db.Categories, "Id", "Name");
            SelectList developers = new SelectList(db.Developers, "Id", "Name");
            ViewBag.Categories = categories;
            ViewBag.Developers = developers;
            if (id == null)
            {
                return HttpNotFound();
            }
            Game game = db.Games.Find(id);
            if (game != null)
            {
                return View(game);
            }
            return HttpNotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditGame(Game game, HttpPostedFileBase productImg, int? developer, int? category)
        {
            /*
            if (game.Image != null)
            {
                var fileName = Path.GetFileName(game.Image);
                var directoryToDelete = Server.MapPath(Url.Content("~/Pictures/Games"));
                var pathToDelete = Path.Combine(directoryToDelete,fileName);
                System.IO.File.Delete(pathToDelete);
            }

             if (productImg != null)
                {
                    var fileName = Path.GetFileName(productImg.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Games"));

                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    productImg.SaveAs(pathToSave);
                    game.Image = fileName;
                }
                    game.CategoryID = category;
                    game.DeveloperID = developer;
                
                db.Games.Add(game);
                db.SaveChanges();
            */
            if (productImg != null)
            {
                var fileName = Path.GetFileName(productImg.FileName);
                var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Games"));
                var pathToSave = Path.Combine(directoryToSave, fileName);
                productImg.SaveAs(pathToSave);
                game.Image = fileName;
            }
            game.CategoryID = category;
            game.DeveloperID = developer;
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Find(id);
            if (category != null)
            {
                return View(category);
            }
            return HttpNotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditCategory(Category category, HttpPostedFileBase productImg)
        {
            if (productImg != null)
            {
                var fileName = Path.GetFileName(productImg.FileName);
                var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Categories"));

                var pathToSave = Path.Combine(directoryToSave, fileName);
                productImg.SaveAs(pathToSave);
                category.Image = fileName;
            }
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditDeveloper(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Developer developer = db.Developers.Find(id);
            if (developer != null)
            {
                return View(developer);
            }
            return HttpNotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditDeveloper(Developer developer, HttpPostedFileBase productImg)
        {
            if (productImg != null)
            {
                var fileName = Path.GetFileName(productImg.FileName);
                var directoryToSave = Server.MapPath(Url.Content("~/Pictures/Developers"));
                var pathToSave = Path.Combine(directoryToSave, fileName);
                productImg.SaveAs(pathToSave);
                developer.Image = fileName;
            }
            db.Entry(developer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}