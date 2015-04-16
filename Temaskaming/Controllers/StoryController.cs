using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class StoryController : Controller
    {
        StoryClass objStory = new StoryClass();

        public ActionResult Index()
        {
            ViewBag.Group = "AboutUs";
            var stories = objStory.GetPublishedStories();
            return View(stories);
        }

        public ActionResult Submit()
        {
            ViewBag.Group = "AboutUs";
            return View();
        }


        public ActionResult SubmitAction()
        {
            ViewBag.Group = "AboutUs";
            if (!ModelState.IsValid) //isvalid checks everything is correct, ex: email, number
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SubmitAction(story story)
        {
            ViewBag.Group = "AboutUs";
            if (ModelState.IsValid)
            {
                try
                {
                    objStory.StorySubmit(story);
                    return View("SubmitAction",story);
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

        public ActionResult Admin()
        {
            ViewBag.Group = "Admin";
            var stories = objStory.GetAllStories();
            return View(stories);
        }

        public ActionResult edit(int id)
        {
            ViewBag.Group = "Admin";
            var story = objStory.GetStoryByID(id);
            if (story == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(story);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult edit(int id, story story)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objStory.EditStory(id, story.author, story.email, story.title, story.story1);
                    return RedirectToAction("Admin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult delete(int id)
        {
            try
            {
                objStory.DeleteStory(id);
                return RedirectToAction("Admin");
            }
            catch
            {
                return View("Admin");
            }
        }

        public ActionResult publish(int id)
        {
            try
            {
                objStory.PublishStory(id);
                return RedirectToAction("Admin");
            }
            catch
            {
                return View("Admin");
            }
        }

        public ActionResult unpublish(int id)
        {
            try
            {
                objStory.UnpublishStory(id);
                return RedirectToAction("Admin");
            }
            catch
            {
                return View("Admin");
            }
        }
    }
}
