using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class linqecardsClass
    {
        //creating an instance of LINQ object
        databaseDataContext objEcard = new databaseDataContext();

        //method which inserts created ecard
        public bool commitInsert(ecard card)
        {
            using (objEcard)
            {
                objEcard.ecards.InsertOnSubmit(card);
                objEcard.SubmitChanges();
                return true;
            }
        }
        //method which inserts created ecard
        public bool commitInsertImage(ecardimage cardimage)
        {
            using (objEcard)
            {
                objEcard.ecardimages.InsertOnSubmit(cardimage);
                objEcard.SubmitChanges();
                return true;
            }
        }
        public IQueryable<ecard> getEcards()
        {
            //creating anonymous varible with value of instance of LINQ object
            var allEcards = objEcard.ecards.Select(x => x);
            return allEcards;
        }
        public IQueryable<ecardimage> getEcardImages()
        {
            //creating anonymous varible with value of instance of LINQ object
            var allEcardImages = objEcard.ecardimages.Select(x => x);
            return allEcardImages;
        }
        //method which gets a review from database by id
        public ecard getEcardById(int _id)
        {
            var allEcards = objEcard.ecards.SingleOrDefault(x => x.Id == _id);
            return allEcards;
        }
        public ecardimage getEcardImageById(int _id)
        {
            var allImages = objEcard.ecardimages.SingleOrDefault(x => x.Id == _id);
            return allImages;
        }
        //method which does update of review
        public bool commitUpdate(int _id, string _sname, string _rname, string _emessage, DateTime _mdate)
        {
            using (objEcard)
            {
                var ecardUpd = objEcard.ecards.Single(x => x.Id == _id);
                ecardUpd.sname = _sname;
                ecardUpd.rname = _rname;
                ecardUpd.emessage = _emessage;
                ecardUpd.mdate = _mdate;
                objEcard.SubmitChanges();
                return true;
            }
        }
        //method which delets a review based on id
        public bool commitDelete(int _id)
        {
            using (objEcard)
            {
                var ecardDel = objEcard.ecards.Single(x => x.Id == _id);
                objEcard.ecards.DeleteOnSubmit(ecardDel);
                objEcard.SubmitChanges();
                return true;
            }
        }
        //method which delets a review based on id
        public bool commitImageDelete(int _id)
        {
            using (objEcard)
            {
                var imageDel = objEcard.ecardimages.Single(x => x.Id == _id);
                objEcard.ecardimages.DeleteOnSubmit(imageDel);
                objEcard.SubmitChanges();
                return true;
            }
        }
    }
}