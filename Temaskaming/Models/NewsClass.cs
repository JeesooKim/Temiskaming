using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class NewsClass
    {
        //use name of the database object
        databaseDataContext objDB = new databaseDataContext();

        //use name of the table (news)
        public IQueryable<news> getNews()
        {
            var allnews = objDB.news.Select(x => x);
            return allnews;
        }

        //get a specific news article by its id
        public news GetNewsByID(int _id)
        {
            var news = objDB.news.SingleOrDefault(x => x.id == _id);
            return news;
        }

        //insert news article of datatype news in database model
        public bool NewsInsert(news news)
        {
            using (objDB)
            {
                objDB.news.InsertOnSubmit(news);
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool NewsUpdate(int _id, string _title, string _content)
        {
            using (objDB)
            {
                var newsUpd = objDB.news.Single(x => x.id == _id);
                newsUpd.title = _title;
                newsUpd.content = _content;
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool NewsDelete(int _id)
        {
            using (objDB)
            {
                var newsDel = objDB.news.Single(x => x.id == _id);
                objDB.news.DeleteOnSubmit(newsDel);
                objDB.SubmitChanges();
                return true;
            }
        }

        public news getLatestNews()
        {
            var news = objDB.news.OrderByDescending(x => x.id).First();
            return news;
        }

    }
}