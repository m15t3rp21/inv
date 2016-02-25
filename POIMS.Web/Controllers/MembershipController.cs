// -----------------------------------------------------------------------
// <copyright file="MembershipController.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using DnrMembershipUser = POIMS.SSO_SAC_Service.Client.BusinessLayer.POIMSMembershipUser;
using DnrMembershipProvider = POIMS.SSO_SAC_Service.Client.BusinessLayer.POIMSMembershipUserProvider;
namespace DNRPS.POIMS.Web.Controllers
{
    using System;
    using System.Data.SqlTypes;
    using System.Web.Mvc;
    using System.Web.Security;
    using DNRPS.POIMS.CommonResources;
    using DNRPS.POIMS.Web.Models;
    using SSO_SAC_Service.Client.BusinessLayer;

    public class MembershipController : BaseController
    {
        #region Actions

        /// <summary>
        /// redirects to login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// validate user credential and redirects to home page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(MembershipModel model, string returnURL)
        {
            if (ModelState.IsValid)
            {
                string userName = model.UserName;
                string password = model.Password;
                string errorMessage = string.Empty;
                if (PerformLogin(userName, password, out errorMessage))
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(userName, false);
                    FormsAuthentication.RedirectFromLoginPage(userName, false);
                    if (!string.IsNullOrEmpty(returnURL))
                    {
                        return Redirect(returnURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(errorMessage.Trim()))
                    {
                        ViewBag.openPopup = WebUtility.ShowAlertMessage(Errors.LoginErrorMessage, Errors.LoginError, WebUtility.JAlertType.Error.ToString());
                    }
                    else
                    {
                        ViewBag.openPopup = WebUtility.ShowAlertMessage(errorMessage, Errors.LoginError, WebUtility.JAlertType.Error.ToString());
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            terminateCurrentAuthSession();

            return Redirect(FormsAuthentication.LoginUrl);
        }

        public ActionResult GetLocations()
        {
            return View();
        }

        /// <summary>
        /// returns the forgot password page
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            string mainURL, alternateURL;
            (Membership.Provider as DnrMembershipProvider).GetUrlToRecoverPassword(new Guid(), Request.Url.Host, out mainURL, out alternateURL);
            return this.RedirectViaRobustRedirection(mainURL, alternateURL);
        }

        /// <summary>
        /// returns the myaccount page
        /// </summary>
        /// <returns></returns>
        public ActionResult MyAccount()
        {
            string mainURL, alternateURL;
            var provider = Membership.Provider as DnrMembershipProvider;
            provider.GetUrlToUpdateMyMembershipAccount(provider.GetMembershipLoginSessionID(this.CurrentUser.UserName), Request.Url.Host, out mainURL, out alternateURL);
            return this.RedirectViaRobustRedirection(mainURL, alternateURL);
        }

        #endregion

        #region UserDefined Methods

        /// <summary>
        /// Attempts to perform Explicit User Login with UserName and Password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="errorMessage"></param>
        /// <returns>true if successful, false otherwise</returns>
        internal bool PerformLogin(string userName, string password, out string errorMessage)
        {
            errorMessage = string.Empty;
            var loginStat = ((DnrMembershipProvider)Membership.Provider).ValidateUser2(userName, password);

            if (loginStat == LoginAttemptStatus.Success)
            {
                // Routines for Successful Membership Login
                var curUser = Membership.GetUser(userName, true) as DnrMembershipUser;

                #region SingleSignOn Procedure: Attach a unique LoginSessionID to current MembershipAccount for cross-App reference.

                Guid NewLoginSessionID = Guid.NewGuid();
                (Membership.Provider as DnrMembershipProvider).SetMembershipLoginSessionID(curUser.UserName, NewLoginSessionID);

                #endregion
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Terminates Current AuthSession
        /// Effect:
        /// 1) Terminates the local User-Authentication Session
        /// 2) Propagate the termination of the AuthSession to any other participating Windows/ASP.NET
        /// 
        /// Usage:
        /// To be called only when user performs an explicit log-out. 
        /// </summary>
        private void terminateCurrentAuthSession()
        {
            try
            {
                // Logout Routines
                // Upon Logout, explicitly set the last-activity date to expire
                // Important: This will result in the termination of all distributed-Authentication-Sessions of the targetted MembershipUser account
                if (this.CurrentUser != null)
                {
                    (Membership.Provider as DnrMembershipProvider).SetUserLastActivityDate((Guid)this.CurrentUser.ProviderUserKey, Convert.ToDateTime(SqlDateTime.MinValue.ToString()));
                }
            }
            catch
            {
            }

            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();

        }

        #endregion
    }
}