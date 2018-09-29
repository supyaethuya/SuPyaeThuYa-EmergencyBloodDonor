using Facebook;
using FacebookLoginTesting.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace FacebookLoginTesting.Controllers
{
    public class UserProfileController : Controller
    {
        private modelEntitiesNew db = new modelEntitiesNew();

       
            // GET: UserProfile
            public ActionResult Index()
        {
            string email = User.Identity.GetUserName();
            var userid = db.UserProfiles.Where(x => x.Email == email).FirstOrDefault().UserId;
            //UserProfile userprofile = db.UserProfiles.Find(userid);


            return View(db.UserProfiles.Where(x => x.UserId == userid).ToList());
            //return View();
        }

        [HttpGet]
        public ActionResult DonorList(string SaveBy, string search)
        {

            if (SaveBy == "BloodType")
            {
         
                return View(db.UserProfiles.Where(x => x.BloodType == search || search==null).ToList());
            }
            else
            {
             
                return View(db.UserProfiles.Where(x => x.Location.StartsWith(search) || search == null).ToList());
            }
           


            //string email = User.Identity.GetUserName();
            ////////UserProfile userprofile = db.UserProfiles.Find(userid);
            //////return View(db.UserProfiles.ToList());
            //return View(db.UserProfiles.Where(x => x.Email != email).ToList());
            ////return View();
        }

  


        // GET: UserProfile/Register
        public ActionResult Register(int? id)
        {
            string tmp_name = User.Identity.GetUserName();
            var userid = db.UserProfiles.Where(x => x.Email == tmp_name).FirstOrDefault().UserId;

            if (userid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(userid);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            if (userprofile.BloodType != null && userprofile.DateOfDonation!=null && userprofile.Location!=null)
            {
              
                return RedirectToAction("Index", "UserProfile");
            }
            return View(userprofile);
        }

 
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,Name,Email,BloodType,Age,PhoneNumber,DateOfDonation,Location,ImagePath")] UserProfile userprofile)
        {
            if (userprofile.Name != null && userprofile.BloodType != null && userprofile.Age != null && 
                userprofile.PhoneNumber != null && userprofile.DateOfDonation != null && userprofile.Location != null )
            {


                if (userprofile.BloodType == "A+" || userprofile.BloodType == "A-" || userprofile.BloodType == "B+" || userprofile.BloodType == "B-"
                      || userprofile.BloodType == "O+" || userprofile.BloodType == "O-" || userprofile.BloodType == "AB+" || userprofile.BloodType == "AB-")
                {


                    if (ModelState.IsValid)
                    {

                        db.Entry(userprofile).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "UserProfile");
                    }

                }
                else
                {

                    ViewBag.Message = "The Blood Type Must Be A+,A-,B+,B-,O+,O-,AB+ or AB-";
                }
            }
            else
            {

                ViewBag.Message = "Please Fill in All of The Text Fields";
            }

                return View(userprofile);
        }




        // GET: UserProfile/EditProfile
        public ActionResult EditProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
          
            
            return View(userprofile);
        }

     
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "UserId,Name,Email,BloodType,Age,PhoneNumber,DateOfDonation,Location,ImagePath")] UserProfile userprofile)
        {
            if (userprofile.Name != null && userprofile.BloodType != null && userprofile.Age != null &&
               userprofile.PhoneNumber != null && userprofile.DateOfDonation != null && userprofile.Location != null)
            {


                if (userprofile.BloodType == "A+" || userprofile.BloodType == "A-" || userprofile.BloodType == "B+" || userprofile.BloodType == "B-"
                      || userprofile.BloodType == "O+" || userprofile.BloodType == "O-" || userprofile.BloodType == "AB+" || userprofile.BloodType == "AB-")
                {


                    if (ModelState.IsValid)
                    {

                        db.Entry(userprofile).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "UserProfile");
                    }

                }
                else
                {

                    ViewBag.Message = "The Blood Type Must Be A+,A-,B+,B-,O+,O-,AB+ or AB-";
                }
            }
            else
            {

                ViewBag.Message = "Please Fill in All of The Text Fields";
            }

            return View(userprofile);
        }


        // GET: UserProfile/Create
        public ActionResult CreatePost()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind(Include = "PostId,BloodType,Age,Location,Illness,PhoneNumber")] Post post)
        {

            if (post.BloodType != null && post.Age != null &&
              post.PhoneNumber != null && post.Illness != null && post.Location != null)
            {


                if (post.BloodType == "A+" || post.BloodType == "A-" || post.BloodType == "B+" || post.BloodType == "B-"
                      || post.BloodType == "O+" || post.BloodType == "O-" || post.BloodType == "AB+" || post.BloodType == "AB-")
                {



                    if (ModelState.IsValid)
                    {
                        string email = User.Identity.GetUserName();
                        UserProfile user = db.UserProfiles.Where(x => x.Email == email).FirstOrDefault();
                        post.UserId = user.UserId;
                        db.Posts.Add(post);
                        db.SaveChanges();
                        return RedirectToAction("Posting");

                    }
                }
                else
                {

                    ViewBag.Message = "The Blood Type Must Be A+,A-,B+,B-,O+,O-,AB+ or AB-";
                }
                }
                else
                {

                    ViewBag.Message = "Please Fill in All of The Text Fields";
                }

                return View();
        }

        // GET: UserProfile/Create
        public ActionResult Posting()
        {

         
            return View(db.Posts);
        }
        

        public ActionResult EditPosting(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
       
                return View(post);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "PostId,UserId,BloodType,Age,Location,Illness,PhoneNumber")] Post post)
        {


            if (post.BloodType != null && post.Age != null &&
              post.PhoneNumber != null && post.Illness != null && post.Location != null)
            {


                if (post.BloodType == "A+" || post.BloodType == "A-" || post.BloodType == "B+" || post.BloodType == "B-"
                      || post.BloodType == "O+" || post.BloodType == "O-" || post.BloodType == "AB+" || post.BloodType == "AB-")
                {



                    if (ModelState.IsValid)
                    {
                        db.Entry(post).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Posting", "UserProfile");

                    }
                }
                else
                {

                    ViewBag.Message = "The Blood Type Must Be A+,A-,B+,B-,O+,O-,AB+ or AB-";
                }
            }
            else
            {

                ViewBag.Message = "Please Fill in All of The Text Fields";

            }

            return View();

        }

        public ActionResult DeletePosting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
    
            if (post == null)
            {
                return HttpNotFound();
            }
           
                return View(post);


        }

        // POST: CRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Posting");
        }



        //public ActionResult DonateBlood(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserProfile userprofile = db.UserProfiles.Find(id);
        //    if (userprofile == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(userprofile);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DonateBlood([Bind(Include = "UserId,Name,Email,BloodType,Age,PhoneNumber,DateOfDonation,Location,ImagePath")] UserProfile userprofile)
        //{
        //    if (userprofile.Name != null && userprofile.BloodType != null && userprofile.Age != null &&
        //       userprofile.PhoneNumber != null && userprofile.DateOfDonation != null && userprofile.Location != null)
        //    {


        //        if (ModelState.IsValid)
        //        {
        //            string email = User.Identity.GetUserName();
        //            UserProfile user = db.UserProfiles.Where(x => x.Email == email).FirstOrDefault();
        //            post.UserId = user.UserId;
        //            db.Posts.Add(post);
        //            db.SaveChanges();
        //            return RedirectToAction("Posting");

        //        }

        //    }
        //    else
        //    {

        //        ViewBag.Message = "Please Fill in All of The Text Fields";
        //    }

        //    return View(userprofile);
        //}

        //public ActionResult Save(FormCollection form)
        //{
        //    string BloodType = form["SaveBy"] + ToString();
        //    ViewBag.BloodType=BloodType;
        //    UserProfile userprofile = new UserProfile();
        //    userprofile.BloodType = BloodType;
        //    db.UserProfiles.Add(userprofile);
        //    db.SaveChanges();
        //    return View(userprofile);

        //    //if (SaveBy == "A+") { 

        //    //userprofile.BloodType = "A+";

        //    //}
        //    //else if (SaveBy == "A-")
        //    //{

        //    //    userprofile.BloodType = "A-";

        //    //}
        //    //else if (SaveBy == "B+")
        //    //{

        //    //    userprofile.BloodType = "B+";

        //    //}
        //    //else if (SaveBy == "B-")
        //    //{

        //    //    userprofile.BloodType = "B-";

        //    //}
        //    //else if (SaveBy == "O+")
        //    //{

        //    //    userprofile.BloodType = "O+";

        //    //}
        //    //else if (SaveBy == "O-")
        //    //{

        //    //    userprofile.BloodType = "O-";

        //    //}
        //    //else if (SaveBy == "AB+")
        //    //{

        //    //    userprofile.BloodType = "AB+";

        //    //}
        //    //else
        //    //{

        //    //    userprofile.BloodType = "AB-";

        //    //}

        //}


    }
}