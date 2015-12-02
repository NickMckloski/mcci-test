using MCCi_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCCi_Test.Controllers
{
    public class DefaultController : Controller
    {

        //list of users
        public static IList<UserModel> users = new List<UserModel>();
        

        // GET: Default
        public ActionResult Index()
        {
            return View(users);
        }


        //new user controller
        public ActionResult NewUser()
        {
            return View();
        }

        //handle post on new user
        [HttpPost]
        public ActionResult NewUser(UserModel user)
        {
            //if model is not valid return the view
            if(!ModelState.IsValid)
                return View(user);

            //add model to the list and redirect to index
            users.Add(user);
            return RedirectToAction("Index");
        }


        //edit user controller
        public ActionResult EditUser(UserModel user)
        {
            //handle post
            if (Request.HttpMethod == "POST")
            {

                //clear errors from the unique id model check since that user is being edited
                ModelState["UserID"].Errors.Clear();

                //if model is not valid return the view
                if (!ModelState.IsValid)
                    return View(user);

                //find and set the new values
                for(int i = 0; i < users.Count(); i++)
                {
                    if(users[i].UserID == user.UserID)
                    {
                        users[i].Prefix = user.Prefix;
                        users[i].Suffix = user.Suffix;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        //delete user controller
        public ActionResult DeleteUser(UserModel user)
        {
            //find and remove the user
            for (int i = 0; i < users.Count(); i++)
            {
                if (users[i].UserID == user.UserID)
                    users.RemoveAt(i);
            }
            return RedirectToAction("Index");
        }
    }
}