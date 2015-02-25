using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class navClass
    {
        databaseDataContext objNav = new databaseDataContext();

        public IQueryable<navigation> getNav(string _group)
        {
            var allNav = objNav.navigations.Select(x => x).Where(x => x.group == _group);
            return allNav;
        }

        
    }
}