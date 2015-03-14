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

        public news GetNewsByID(int _id)
        {
            var news = objDB.news.SingleOrDefault(x => x.article_id == _id);
            return news;
        }

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
                var newsUpd = objDB.news.Single(x => x.article_id == _id);
                newsUpd.title = _title;
                newsUpd.content = _content;
                return true;
            }
        }

        public bool NewsDelete(int _id)
        {
            using (objDB)
            {
                var newsDel = objDB.news.Single(x => x.article_id == _id);
                objDB.news.DeleteOnSubmit(newsDel);
                objDB.SubmitChanges();
                return true;
            }
        }
        
    }
}