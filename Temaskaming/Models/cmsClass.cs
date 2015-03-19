using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public bool createPage(string path, string content, navigation nav)
        {
            File.WriteAllText(path, content);
            using (objCMS)
            {
                objCMS.navigations.InsertOnSubmit(nav);
                objCMS.SubmitChanges();
                return true;
            }
            
        }

        public bool deletePage(string path, int _id)
        {
            File.Delete(path);
            using (objCMS)
            {
                var page = objCMS.navigations.SingleOrDefault(x => x.id == _id);
                objCMS.navigations.DeleteOnSubmit(page);
                objCMS.SubmitChanges();
                return true;
            }
        }

        public bool updatePage(string path, string content, int _id, string _name, string _group)
        {
            File.WriteAllText(path, content);
            if (_group == "")
            {
                _group = null;
            }
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

        public IEnumerable<SelectListItem> allGroups()
        {
            using (objCMS)
            {
                var group = objCMS.navigations.Select(x => x.group).Distinct().ToArray();
                List<SelectListItem> something = new List<SelectListItem>();
                foreach (var g in group)
                {
                    if (g != "Home" && g != "Admin") {
                        if (g == "AboutUs")
                        {
                            var keyval = new SelectListItem { Value = g, Text = "About Us" };
                            something.Add(keyval);
                        }
                        else if (g == "JoinOurTeam")
                        {
                            var keyval = new SelectListItem { Value = g, Text = "Join Our Team" };
                            something.Add(keyval);
                        }
                        else if (g == "Patients")
                        {
                            var keyval = new SelectListItem { Value = g, Text = "Patients and Visitors" };
                            something.Add(keyval);
                        }
                        else if (g == "ProgramsServices")
                        {
                            var keyval = new SelectListItem { Value = g, Text = "Programs and Services" };
                            something.Add(keyval);
                        }
                        else if (g == "ContactUs")
                        {
                            var keyval = new SelectListItem { Value = g, Text = "Contact Us" };
                            something.Add(keyval);
                        }
                        else
                        {
                            var keyval = new SelectListItem { Value = g, Text = g };
                            something.Add(keyval);
                        }
                    }
                }
                return something;
            }
        }
    }
}