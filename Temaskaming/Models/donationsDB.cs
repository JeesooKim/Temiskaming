using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class donationsDB
    {
        databaseDataContext objDon = new databaseDataContext();

        public IQueryable<donation> getAllDonations()
        {
            var donations = objDon.donations.Select(x => x);
            return donations;
        }

        public donation getDonationById(int _id)
        {
            var donation = objDon.donations.SingleOrDefault(x => x.id == _id);
            return donation;
        }
    }
}