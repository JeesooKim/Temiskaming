using System;
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
    }
}