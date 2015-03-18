using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class cmsClass
    {
        databaseDataContext objCMS = new databaseDataContext();

        public IQueryable<navigation> getPages()
        {
            var allPages = objCMS.navigations.Select(x => x);
            return allPages;
        }

        public navigation getPage(int _id)
        {
            var page = objCMS.navigations.SingleOrDefault(x => x.id == _id);
            return page;
        }

        public int getLastId()
        {
            var pageId = objCMS.navigations.Max(x => x.id);
            return pageId;
        }

        public bool createPage(string path,string content, navigation nav)
        {
            File.WriteAllText(path, content);
            using (objCMS)
            {
                objCMS.navigations.InsertOnSubmit(nav);
                objCMS.SubmitChanges();
                return true;
            }
            
        }

        public bool deletePage(int _id)
        {
            using (objCMS)
            {
                var page = objCMS.navigations.SingleOrDefault(x => x.id == _id);
                objCMS.navigations.DeleteOnSubmit(page);
                objCMS.SubmitChanges();
                return true;
            }
        }

        public bool updatePage(int _id, string _name, string _group)
        {
            using (objCMS)
            {
                var page = objCMS.navigations.SingleOrDefault(x => x.id == _id);
                page.name = _name;
                page.group = _group;
                page.viewpath = _name;
                objCMS.SubmitChanges();
                return true;
            }
        }
    }
}