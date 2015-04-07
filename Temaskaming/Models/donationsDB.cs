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

        public donation getLatestDonation()
        {
            var donation = objDon.donations.OrderByDescending(x => x.id).First();
            return donation;
        }

        public IQueryable<donation> getVerifiedDonations()
        {
            var received = objDon.donations.Where(x => x.verify == true);
            return received;
        }

        public bool deleteAllUnverified()
        {
            using (objDon)
            {
                var unverified = objDon.donations.Where(x => x.verify == false && (DateTime.UtcNow - (DateTime)x.donation_date).TotalMinutes > 1);
                objDon.donations.DeleteAllOnSubmit(unverified);
                objDon.SubmitChanges();
                return true;
            }
        }

        public bool makeDonation(donationsPublic donation)
        {
            donation don = new donation();
            don.first_name = donation.fname;
            don.last_name = donation.lname;
            don.email = donation.email;
            don.amount = donation.amount;
            don.message = donation.message;
            don.donation_date = DateTime.Now;
            objDon.donations.InsertOnSubmit(don);
            objDon.SubmitChanges();
            return true;
        }

        public bool verifyDonation(int _id)
        {
            using (objDon)
            {
                var donation = objDon.donations.SingleOrDefault(x => x.id == _id);
                if (donation.verify == false)
                {
                    donation.verify = true;
                }
                objDon.SubmitChanges();
                return true;
            }
        }

        public bool deleteDonation(int _id)
        {
            using (objDon)
            {
                var donation = objDon.donations.SingleOrDefault(x => x.id == _id);
                objDon.donations.DeleteOnSubmit(donation);
                objDon.SubmitChanges();
                return true;
            }
        }
    }
}