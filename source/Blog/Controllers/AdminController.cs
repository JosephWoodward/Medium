﻿using System;
using System.Web.Mvc;
using Blog.Core.Security;
using Blog.Core.Service;
using Blog.Models.AdminModel;

namespace Blog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAuthenticationProvider _authProvider = new AuthenticationProvider();
        private readonly BlogService _service = new BlogService();

        public ActionResult Index()
        {
            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var authenticated = _authProvider.Authenticate(model.Username, model.Password);
                if (authenticated)
                {
                    return Redirect(Request.QueryString["ReturnUrl"]);
                }

                ModelState.AddModelError(string.Empty, "Username or Password Incorrect");
            }

            return View(model);
        }

        public ActionResult AddBlogEntry(BlogEntryModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = _service.AddBlogEntry(model.Header, model.HeaderSlug, model.Content);
                if (success)
                {
                    return RedirectToAction("Index", "Blog");
                }

                ModelState.AddModelError(string.Empty, "something went wrong andre");
            }

            return View("Index");
        }

    }
}