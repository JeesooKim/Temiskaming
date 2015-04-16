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
            var stories = objStory.GetPublishedStories();
            return View(stories);
        }

        public ActionResult Submit()
        {
            return View();
        }

        public ActionResult SubmitAction()
        {
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

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Group = "Admin";
            var stories = objStory.GetAllStories();
            return View(stories);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult delete(int id)
        {
            ViewBag.Group = "Admin";
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

        [Authorize(Roles = "Admin")]
        public ActionResult publish(int id) //publish an article
        {
            ViewBag.Group = "Admin";
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

        [Authorize(Roles = "Admin")]
        public ActionResult unpublish(int id) //take a published article off
        {
            ViewBag.Group = "Admin";
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
