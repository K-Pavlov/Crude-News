using CrudeNews.Data;
using CrudeNews.Helpers;
using CrudeNews.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
namespace CrudeNews.Web.Controllers
{
    public class ManageController : BaseController
    {
        public ManageController(ICrudeNewsData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var model = this.data
                .Users
                .All()
                .Project()
                .To<UserViewModel>()
                .FirstOrDefault(x => x.Username == this.User.Identity.Name);

            return View(model);
        }

        public ActionResult ChangePassword(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                    : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                    : message == ManageMessageId.Error ? "An error has occurred."
                    : "";

            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvatar(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (!ImageProcessor.IsValidImage(file))
                {
                    TempData["AvatarError"] = "Please upload an image file";
                    return this.RedirectToAction("Manage");
                }

                string folderLocation = "~/Content/Avatars";
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string fullName = fileName + "_" + GetRandomString(16) + fileExtension;
                string path = Path.Combine(
                    Server.MapPath(folderLocation), fullName);

                ImageProcessor.SaveAsAvatar(file, path);

                var currentUser = this.data.Users
                    .All()
                    .FirstOrDefault(x => x.UserName == this.User.Identity.Name);

                if (currentUser != null)
                {
                    currentUser.AvatarPath = folderLocation + "/" + fullName;
                }

                this.data.Users.Update(currentUser);
                this.data.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangeAge(int age)
        {
            var userName = this.User.Identity.Name;
            var user = this.data.Users.All().FirstOrDefault(x => x.UserName == userName);
            user.Age = age;
            this.data.SaveChanges();

            ////Paranoia
            if (user == null)
            {
                this.TempData["UserError"] = "Doge didn't expect that. Wow.";
                return this.View("Error");
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewProfile(string id)
        {
            if (id != null)
            {
                var user = this.data.Users.Find(id);
                var model = AutoMapper.Mapper.Map<UserViewModel>(user);

                return View(model);
            }
            else
            {
                var currentUser = this.data.Users
                    .All()
                    .Project()
                    .To<UserViewModel>()
                    .FirstOrDefault(x => x.Username == this.User.Identity.Name);

                if (currentUser != null)
                {
                    return View(currentUser);
                }
                else
                {
                    return View("Index", "Home");
                }
            }
        }

        private string GetRandomString(int size)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        private bool HasPassword()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<CrudeNewsUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }
    }
}