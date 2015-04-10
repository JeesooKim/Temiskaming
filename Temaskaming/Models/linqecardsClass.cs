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
        // method which gets all Ecards
        public IQueryable<ecard> getEcards()
        {
            var allEcards = objEcard.ecards.Select(x => x);
            return allEcards;
        }
        // method which gets all Ecard images
        public IQueryable<ecardimage> getEcardImages()
        {
            var allEcardImages = objEcard.ecardimages.Select(x => x);
            return allEcardImages;
        }
        //method which gets Ecard by Id
        public ecard getEcardById(int _id)
        {
            var allEcards = objEcard.ecards.SingleOrDefault(x => x.Id == _id);
            return allEcards;
        }
        //method which gets ecard images by id
        public ecardimage getEcardImageById(int _id)
        {
            var allImages = objEcard.ecardimages.SingleOrDefault(x => x.Id == _id);
            return allImages;
        }
        //method which updates Ecard
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
        //method which deletes a Ecard based on id
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
        //method which deletes image based on id
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