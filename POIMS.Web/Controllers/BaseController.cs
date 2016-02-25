// -----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using DnrMembershipProvider = POIMS.SSO_SAC_Service.Client.BusinessLayer.POIMSMembershipUserProvider;
using DnrMembershipUser = POIMS.SSO_SAC_Service.Client.BusinessLayer.POIMSMembershipUser;
namespace DNRPS.POIMS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;
    using DNRPS.POIMS.Business;
    using DNRPS.POIMS.CommonResources;
    using DNRPS.POIMS.CommonResources.Enums;
    using DNRPS.POIMS.Web.Models;
    using DnrSwLic = FTSWLic.FTSWLic;

    public class BaseController : Controller
    {
        #region Fields

        private const string QUERY_AUTHSESSION_ID = "AuthSessionId";
        private const string QUERY_GLOBAL_RETURN_URL = "ReturnUrl";

        private const string AppConfigDistributedAuthenticationSessionTimeoutDurationInMins = "DistributedAuthenticationSessionTimeoutDurationInMins";

        #endregion

        internal DnrMembershipUser CurrentUser
        {
            get
            {
                return getCurrentUserAndKeepAuthSessionAlive();
            }
        }

        public static string WelcomeMessageRawHtml
        {
            get
            {
                var curUser = getCurrentUserAndKeepAuthSessionAlive();
                var name = Names.Guest;
                if (curUser != null)
                {
                    name = curUser.PersonnelProfileFullName;
                }

                return string.Format("{0}(<i>UTC {1}</i>)", string.Format(Messages.HtmlWelcomeUserName_Arg0Name, name), DateTime.Now.ToString("zzz"));
            }
        }

        #region Private SSO Utility
        /// <summary>
        /// Obtain information of the MembershipUser Account corresponding to Current Logged-In User
        ///  Instructions: 
        ///      1) This function should be invoked on each post-back to keep the Current AuthSessionAlive. Prolonged
        ///          delay in calling this function will result in the expiration of the current AuthSession.
        ///  Purpose: Synchronize Distributed-AuthSessions
        ///      2) If the current AuthSession is distributed in nature, ie. shared with one or more Windows/ASP.NET application,
        ///          then calling this function in any of the participating Windows/ASP.NET application will effectively attempt to
        ///          keep the DistributedAuthSession Alive
        ///      3) This function will also detect and response to the remote-termination of the current AuthSession, ie. user explicitly logoff from
        ///          this Distributed-AuthSession from any of the participating Windows/ASP.NET application. 
        /// </summary>
        /// <returns>
        ///     If Current AuthSession is alive: 
        ///         returns an instance of FTSSOMembershipUser representing the MembershipAccount of the current logged-in User
        ///     else
        ///         returns null
        /// </returns>
        private static DnrMembershipUser getCurrentUserAndKeepAuthSessionAlive()
        {
            // Calling this function will implicitly update the LastActivityDate Field of the target MembershipUser account
            // effectively keeping the authentication-session alive
            var currentUser = Membership.GetUser() as DnrMembershipUser;
            if (currentUser != null)
            {
                double sessionTimeoutInMins = Convert.ToDouble(ConfigurationManager.AppSettings[AppConfigDistributedAuthenticationSessionTimeoutDurationInMins]);

                // verify that the current AuthSession has not-expired yet
                TimeSpan sessionIdlePeriod = DateTime.Now - currentUser.PreviousLastActivityDate;
                if (sessionIdlePeriod.TotalMinutes > sessionTimeoutInMins)
                {
                    // AuthSession Has Expired!
                    // Possible Reasons: 
                    //  1) Prolonged inactivity 
                    //  2) Remote-termination of the current AuthSession, applies to Distributed-AuthSession only

                    // Response: To explicitly terminate the local ASP.NET authentication session
                    FormsAuthentication.SignOut();
                    return null;
                }
                else
                {
                    // AuthSession is still active
                    return currentUser;
                }
            }
            else
            {
                // Anonymous User
                return null;
            }
        }

        /// <summary>
        /// Initiates a Distributed-AuthSession
        /// 
        /// Effect:
        ///     1) Performs an implicit local User-Authentication Session by referencing the details of the specified DistributedAuthSession
        /// 
        /// Prerequisites for Successful Initiation of a Distributed-AuthSession:
        ///     1) The Distributed-AuthSession must be successfully retrieved from the SSO-SAC-Service
        ///     2) The Distributed-AuthSession must be considered active in local-context, ie. the elasped time since the last-activity timestamp of the
        ///         specified Distributed-AuthSession must not exceed the locally defined AuthSessionTimeout duration
        /// </summary>
        /// <param name="authenticationSessionId"></param>
        /// <returns>true if successful, false otherwise</returns>
        private bool initiateDistributedAuthSession(Guid authenticationSessionId, out RedirectResult redirectAction)
        {
            Guid userId;
            DateTime lastActivityUTCTS;
            redirectAction = new RedirectResult("~/");
            try
            {
                (Membership.Provider as DnrMembershipProvider).GetMembershipUserIDAndLastActivityTSByLoginSessionID(authenticationSessionId, out userId, out lastActivityUTCTS);

                // verify that the specified login-session has not-expired yet
                TimeSpan sessionIdlePeriod = DateTime.Now.Subtract(lastActivityUTCTS.ToLocalTime());
                double sessionTimeoutInMins = Convert.ToDouble(ConfigurationManager.AppSettings[AppConfigDistributedAuthenticationSessionTimeoutDurationInMins]);

                if (sessionIdlePeriod.TotalMinutes > sessionTimeoutInMins)
                {
                    // Distributed-AuthSession Timed-Out in local context
                    FormsAuthentication.SignOut();
                    return false;
                }
                else
                {
                    // Distributed-AuthSession is considered active in local context  
                    // Allow Implicit User Login
                    // Routines for Successful Membership Login
                    #region Set CurrentUser as Current Principal in Current Thread-Context for application-wide reference

                    FormsAuthentication.SignOut();
                    var currentUser = Membership.GetUser(userId, true) as DnrMembershipUser;
                    FormsAuthentication.SetAuthCookie(currentUser.UserName, false);
                    #region Navigate to the appropriate page

                    if (!string.IsNullOrEmpty(Request.QueryString[QUERY_GLOBAL_RETURN_URL]))
                    {
                        #region Removes QueryStringAuthSessionId from returnUrl before navigating to the ReturnUrl Page
                        string returnUrl = Request.QueryString[QUERY_GLOBAL_RETURN_URL];
                        char[] delimiter_URL_QueryString = { '?' };
                        string[] returnUrlSegments = returnUrl.Split(delimiter_URL_QueryString);
                        string returnUrlWithoutQueryString = returnUrlSegments[0];
                        string queryStrings = returnUrlSegments[1];
                        char[] delimiter_QueryStringElement = { '&' };
                        string[] querystringsArray = queryStrings.Split(delimiter_QueryStringElement);
                        if (querystringsArray.Length > 1)
                        {
                            // There are other querystrings embedded within the returnUrl apart from QueryStringAuthSessionId
                            StringBuilder redirectUrlBuilder = new StringBuilder(returnUrlWithoutQueryString);

                            // Restore all the other QueryStrings except QueryStringAuthSessionId
                            redirectUrlBuilder.Append("?");
                            for (int i = 0; i < querystringsArray.Length; i++)
                            {
                                string queryStringKEY = querystringsArray[i].Remove(querystringsArray[i].IndexOf('='));
                                if (!queryStringKEY.Equals(QUERY_AUTHSESSION_ID))
                                {
                                    redirectUrlBuilder.Append(querystringsArray[i] + "&");
                                }
                            }

                            redirectAction = new RedirectResult(redirectUrlBuilder.ToString().Remove(redirectUrlBuilder.ToString().Length - 1));
                            ////Response.Redirect(redirectUrlBuilder.ToString().Remove(redirectUrlBuilder.ToString().Length - 1), false);
                        }
                        else
                        {
                            redirectAction = new RedirectResult(returnUrlWithoutQueryString);
                            ////Response.Redirect(returnUrlWithoutQueryString, false);
                        }
                        #endregion
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString[QUERY_AUTHSESSION_ID]))
                    {
                        #region Removes QueryStringAuthSessionId from Request's Url before navigating to the ReturnUrl Page

                        int startIndex = Request.Url.AbsoluteUri.IndexOf('?');
                        string curUrlWithoutQueryString = Request.Url.AbsoluteUri.Remove(startIndex);
                        if (Request.QueryString.Count > 1) /* There are other querystring apart from QueryString[QueryStringVarName_AuthSessionId] */
                        {
                            StringBuilder redirectUrlBuilder = new StringBuilder(curUrlWithoutQueryString);

                            // Restore all the other QueryStrings except QueryString[QueryStringVarName_AuthSessionId]
                            redirectUrlBuilder.Append("?");
                            for (int i = 0; i < Request.QueryString.Count; i++)
                            {
                                if (!Request.QueryString.Keys[i].Equals(QUERY_AUTHSESSION_ID))
                                {
                                    redirectUrlBuilder.Append(Request.QueryString.Keys[i] + "=" + Request.QueryString[i] + "&");
                                }
                            }

                            redirectAction = new RedirectResult(redirectUrlBuilder.ToString().Remove(redirectUrlBuilder.ToString().Length - 1));
                            ////Response.Redirect(redirectUrlBuilder.ToString().Remove(redirectUrlBuilder.ToString().Length - 1), false);
                        }
                        else
                        {
                            redirectAction = new RedirectResult(curUrlWithoutQueryString);
                            ////Response.Redirect(curUrlWithoutQueryString, false);
                        }
                        #endregion
                    }

                    #endregion

                    #endregion

                    return true;
                }
            }
            catch ////(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Active session check

        /// <summary>
        /// The on result executing.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();
            base.OnResultExecuting(filterContext);
        }


        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                System.Web.HttpContext ctx = System.Web.HttpContext.Current;
                Uri url = Request.Url;
                string mainUrl = string.Empty;

                // Checking URL with query or for virtual directory
                if (!string.IsNullOrEmpty(url.Query))
                {
                    mainUrl = (url.Authority + ctx.Request.ApplicationPath).Trim('/') + "/" + url.Query;
                }
                else
                {
                    mainUrl = (url.Authority + ctx.Request.ApplicationPath).Trim('/');
                }

                if (!url.AbsoluteUri.Trim('/').EndsWith(mainUrl) && !Request.Url.AbsolutePath.ToUpper().Contains("/LOGIN") && !Request.Url.AbsolutePath.ToUpper().Contains("/LOGOUT")
                    && !Request.Url.AbsolutePath.ToUpper().Contains("/FORGOTPASSWORD") && !Request.Url.AbsolutePath.ToUpper().Contains("/REDIRECTPOPUP"))
                {
                    // Supports the initiation of DNR-SSO-SAC Distributed-AuthSession
                    var hasDistributedAuth = false;
                    if (this.Request.RequestType == "GET" && !string.IsNullOrEmpty(Request.QueryString[QUERY_AUTHSESSION_ID]))
                    {
                        RedirectResult resultAction;
                        hasDistributedAuth = this.initiateDistributedAuthSession(new Guid(Request.QueryString[QUERY_AUTHSESSION_ID]), out resultAction);
                        if (hasDistributedAuth)
                        {
                            filterContext.Result = resultAction;
                        }
                    }

                    if (!hasDistributedAuth)
                    {
                        var currentUser = getCurrentUserAndKeepAuthSessionAlive();
                        if (currentUser != null)
                        {
                            // Verifies License
                            WebUtility.ApplicationErrorMessage = null;
                            string htmlErrorMessage;
                            if (!this.isCompliantWithLicense(out htmlErrorMessage))
                            {
                                // Only go to the error page if current page is NOT already the error page
                                if (!Request.Url.PathAndQuery.EndsWith("Error/Index"))
                                {
                                    WebUtility.ApplicationErrorMessage = htmlErrorMessage;
                                    filterContext.Result = new RedirectResult(Url.Content("~/Error/Index"));
                                }
                            }
                        }
                        else
                        {
                            System.Web.Security.FormsAuthentication.SignOut();
                            Session.Abandon();
                            if (filterContext.HttpContext.Request.IsAjaxRequest())
                            {
                                // For AJAX requests, we're overriding the returned JSON result with a simple string,
                                // indicating to the calling JavaScript code that a redirect should be performed.
                                filterContext.Result = new JsonResult { Data = "_Login_" };
                                return;
                            }
                            else
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Membership" }, { "Action", "Login" }, { "returnURL", Request.Url.ToString() } });
                            }
                        }
                    }
                }
            }
            catch
            {
                Response.Redirect("~/Error/WithoutLogin");
            }

            base.OnActionExecuting(filterContext);

        }


        #endregion

        #region License Policy

        private DnrSwLic loadLicense(out string htmlErrorMessage)
        {
            htmlErrorMessage = string.Empty;
            if (ApplicationCache.AppCache.Instance["License"] == null)
            {
                try
                {
                    ApplicationCache.AppCache.Instance["License"] = new DnrSwLic(
                        new Guid(ConfigurationManager.AppSettings["LicenseKEY"]),
                        ConfigurationManager.AppSettings["ClaimedLicensedSW"],
                        ConfigurationManager.AppSettings["ClaimedLicensee"]);
                }
                catch (Exception ex)
                {
                    htmlErrorMessage = ex.Message;
                }
            }

            return ApplicationCache.AppCache.Instance["License"] as DnrSwLic;
        }

        private bool isCompliantWithLicense(out string htmlErrorMessage)
        {
            var license = this.loadLicense(out htmlErrorMessage);
            if (license == null)
            {
                htmlErrorMessage = string.Format(Errors.HtmlErrorLoadingSoftwareLicense_Arg0ErrorDetail, htmlErrorMessage);
                return false;
            }

            if (!license.IsLicenseValid)
            {
                htmlErrorMessage = string.Format(Errors.HtmlErrorLoadingSoftwareLicense_Arg0ErrorDetail, license.LicenseValidityStatement);
                return false;
            }

            this.RecomputeAndValidateActiveUsers();

            htmlErrorMessage = string.Empty;
            return true;
        }

        private Dictionary<string, string> getLicensePolicyRules()
        {
            if (ApplicationCache.AppCache.Instance["LicensePolicyRules"] == null)
            {
                string dummy;
                var license = this.loadLicense(out dummy);
                if (license != null)
                {
                    ApplicationCache.AppCache.Instance["LicensePolicyRules"] = license.GetLicensePolicyRules();
                }
            }

            return ApplicationCache.AppCache.Instance["LicensePolicyRules"] as Dictionary<string, string>;
        }

        /// <summary>
        /// Automatically redirects to the restricted pages if the number of maximum user accounts has been exceeded
        /// </summary>
        internal void RecomputeAndValidateActiveUsers()
        {
            var policies = this.getLicensePolicyRules();
        }
        #endregion

        /// <summary>
        /// Utility to match the user access privilege(s).
        /// Outputs the RedirectResult for the relevant page if privilege does not match.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="requiredPrivileges"></param>
        /// <param name="errorRedirectionResult"></param>
        internal bool PerformMatchPrivilege(AccessPrivilege.PrivilegeMatchCriteria criteria, List<AccessPrivilegeType.EnumLiteral> requiredPrivileges, out RedirectResult errorRedirectionResult)
        {
            errorRedirectionResult = null;
            if (this.CurrentUser == null)
            {
                errorRedirectionResult = Redirect(FormsAuthentication.LoginUrl);
            }
            else
            {
                string htmlErrorMessage;
                if (!AccessPrivilege.MatchPrivilege(criteria, requiredPrivileges, this.CurrentUser.InheritedPriviledges, out htmlErrorMessage))
                {
                    WebUtility.ApplicationErrorMessage = htmlErrorMessage;
                    errorRedirectionResult = RedirectToErrorPage();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// returns the Redirect Popup
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectPopup(string RedirSK)
        {
            var redirectModel = new RedirectModel();
            if (!string.IsNullOrEmpty(RedirSK))
            {
                string sessionKey = RedirSK;
                var redirectionUrls = Session[sessionKey] as List<string>;
                Session[sessionKey] = null; // deletes away session holding the urls
                if (redirectionUrls.Count > 0)
                {
                    string mainUrl = redirectionUrls[0];
                    string alternateUrl = (redirectionUrls.Count > 1) ? redirectionUrls[1] : mainUrl;
                    string hostNameOfMainUrl = string.Empty;

                    // Extracts Hostname of url, ie. hostname1
                    if (mainUrl.ToLower().Contains("http://"))
                    {
                        hostNameOfMainUrl = mainUrl.Split('/')[2]; // skip preceding http://
                    }
                    else
                    {
                        hostNameOfMainUrl = mainUrl.Split('/')[0];
                    }

                    redirectModel.HostNameOfMainUrl = hostNameOfMainUrl;
                    redirectModel.MainUrl = mainUrl;
                    redirectModel.AlternateUrl = alternateUrl;
                }
            }

            return View(redirectModel);
        }

        public ActionResult ViewAuditTrail(int siteKey, int auditTrailKey)
        {
            string errorHeader, errorDetails, mainURL, alternateURL;
            AuditTrailLogger.GetURLForAuditTrailArchive(siteKey, auditTrailKey, true, Request.Url.Host, out errorHeader, out errorDetails, out mainURL, out alternateURL);
            return this.RedirectViaRobustRedirection(mainURL, alternateURL, true);
        }

        /// <summary>
        /// Redirect URL for Error Page
        /// </summary>
        /// <returns></returns>
        public RedirectResult RedirectToErrorPage()
        {
            return Redirect(Url.Content("~/Error/Index"));
        }

        /// <summary>
        /// Returns RedirectResult to go to the page to handle url redirection of mainUrl/alternateUrl.
        /// </summary>
        /// <param name="mainUrl"></param>
        /// <param name="alternateUrl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RedirectResult RedirectViaRobustRedirection(string mainUrl, string alternateUrl, bool isPropagateLoginSession = false)
        {
            if (isPropagateLoginSession)
            {
                var provider = Membership.Provider as DnrMembershipProvider;
                var authId = provider.GetMembershipLoginSessionID(this.CurrentUser.UserName);
                mainUrl = string.Format("{0}&{1}={2}", mainUrl, QUERY_AUTHSESSION_ID, authId);
                alternateUrl = string.Format("{0}&{1}={2}", alternateUrl, QUERY_AUTHSESSION_ID, authId);
            }

            return Redirect("~" + prepareForRobustUrlRedirection(mainUrl, alternateUrl));
        }

        /// <summary>
        /// Primary Helper function for Retrying the alternative URL if Preferred URL is unreachable!
        /// </summary>
        /// <param name="mainUrl"></param>
        /// <param name="alternateUrl"></param>
        /// <returns></returns>
        private string prepareForRobustUrlRedirection(string mainUrl, string alternateUrl)
        {
            string pathOfRedirHelperPage = "/Membership/RedirectPopup"; // Must be relative to the application root
            string tempSessionKey = Guid.NewGuid().ToString().Replace("-", ""); // Generates a unique key for a temporary SessionVariable to hold the Urls. This Session Variable will be automatically destroyed after use.
            Session[tempSessionKey] = new List<string> { mainUrl, alternateUrl };
            return string.Format("{0}/?RedirSK={1}", pathOfRedirHelperPage, tempSessionKey);
        }
    }
}