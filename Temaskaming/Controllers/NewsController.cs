using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class NewsController : Controller
    {

        NewsClass objNews = new NewsClass();

        
        public ActionResult Index()
        {
            ViewBag.Group = "AboutUs";
            var news = objNews.getNews();
            return View(news);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult newsadmin()
        {
            ViewBag.Group = "Admin";
            var news = objNews.getNews(); //Get all the news articles from the database.
            return View(news);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult newsupdate(int id)
        {
            ViewBag.Group = "Admin";
            var news = objNews.GetNewsByID(id); //Get the specific news article by its ID in the database
            if (news == null)
            {
                return View("NotFound"); //If the article not found, return to NotFound page.
            }
            else
            {
                return View(news);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)] //validate input needs to be turned off in order to store text that is edited by CKEditor
        public ActionResult newsupdate(int id, news news) //update specific news article
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.NewsUpdate(id, news.title, news.content);
                    return RedirectToAction("newsadmin"); //after editing, return to admin page.
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult newsupdatePartial(int id)
        {
            var news = objNews.GetNewsByID(id);
            if (news == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(news);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)]
        public ActionResult newsupdatePartial(int id, news news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.NewsUpdate(id, news.title, news.content);
                    return RedirectToAction("newsadmin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult newsdelete(int id)
        {
            ViewBag.Group = "Admin";
            var news = objNews.GetNewsByID(id);
            if (news == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(news);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult newsdelete(int id, news news)
        {
            ViewBag.Group = "Admin";
            try
            {
                objNews.NewsDelete(id);
                return RedirectToAction("newsadmin"); //after deleting the news article, return to admin page.
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult newsinsert()
        {
            ViewBag.Group = "Admin";
            return View(); //display the page to create new articles
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)]
        public ActionResult newsinsert(news news) //store the article into the database
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.NewsInsert(news);
                    return RedirectToAction("newsadmin");
                }

                catch
                {
                    return View();
                }
            }

            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NotFound()
        {
            ViewBag.Group = "Admin";
            return View();
        }

    }
}
