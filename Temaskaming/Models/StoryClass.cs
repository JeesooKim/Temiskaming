using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class StoryClass
    {
        databaseDataContext objDB = new databaseDataContext();

        public IQueryable<story> GetAllStories()
        {
            var allStories = objDB.stories.Select(x => x);
            return allStories;
        }

        public story GetStoryByID(int id)
        {
            var story = objDB.stories.SingleOrDefault(x => x.id == id);
            return story;
        }

        public bool EditStory(int id, string author, string email, string title, string story)
        {
            using (objDB)
            {
                var storyEdit = objDB.stories.Single(x => x.id == id);
                storyEdit.author = author;
                storyEdit.email = email;
                storyEdit.title = title;
                storyEdit.story1 = story;
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool DeleteStory(int id)
        {
            using (objDB)
            {
                var storyDel = objDB.stories.Single(x => x.id == id);
                objDB.stories.DeleteOnSubmit(storyDel);
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool PublishStory(int id)
        {
            using (objDB)
            {
                var story = objDB.stories.Single(x => x.id == id);
                story.published = 1;
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool UnpublishStory(int id)
        {
            using (objDB)
            {
                var story = objDB.stories.Single(x => x.id == id);
                story.published = 0;
                objDB.SubmitChanges();
                return true;
            }
        }

        public IQueryable<story> GetPublishedStories()
        {
            var allStories = objDB.stories.Where(x => x.published == 1);
            return allStories;
        }

        public bool StorySubmit(story story)
        {
            using (objDB)
            {
                objDB.stories.InsertOnSubmit(story);
                objDB.SubmitChanges();
                return true;
            }
        }
    }
}