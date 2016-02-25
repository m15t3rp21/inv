// -----------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            Exception exception = Server.GetLastError();
            string errorMessage = string.Empty;
            if (exception == null && !string.IsNullOrEmpty(WebUtility.ApplicationErrorMessage))
            {
                errorMessage = WebUtility.GetApplicationErrorMessageAndRemoveFromSession();
            }

            ViewBag.Error = exception != null ? exception.ToString() : errorMessage;
            Server.ClearError();
            return View();
        }

        public ActionResult PageNotFound()
        {
            ViewBag.HideonError = true;
            return View();
        }

        public ActionResult WithoutLogin()
        {
            return View();
        }
    }
}