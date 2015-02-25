using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class adminClass
    {
        databaseDataContext objAdmin = new databaseDataContext();

        public IQueryable<admin> getAllAdmin()
        {
            var allAdmin = objAdmin.admins.Select(x => x);
            return allAdmin;
        }

        public int getAdminID(string _login, string _pass)
        {
            var admin = objAdmin.admins.SingleOrDefault(x => x.login == _login && x.pass == _pass);
            return admin.id;
        }
    }
}