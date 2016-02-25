namespace DNRPS.POIMS.Web
{
    using System;
    using System.Web;
    using DNRPS.POIMS.CommonResources;

    public class WebUtility
    {
        /// <summary>
        /// Page session name for generalize message
        /// </summary>
        public enum PageSessionName
        {
            ChemicalIndex,
            ChemicalCreateEdit,
            ReplenishmentOrderIndex,
            ReplenishmentOrderCreateEdit,
            InventoryIndex
        }

        /// <summary>
        /// Message Type for prompt
        /// </summary>
        public enum MessageType
        {
            Success,
            Error,
            HTMLError
        }

        /// <summary>
        /// Alert message
        /// </summary>
        public enum JAlertType
        {
            Alert,
            Error,
            Confirm,
            Help
        }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        public static string ApplicationErrorMessage
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["ApplicationErrorMessage"]);
            }

            set
            {
                HttpContext.Current.Session["ApplicationErrorMessage"] = value;
            }
        }

        public static string GetApplicationErrorMessageAndRemoveFromSession()
        {
            var message = ApplicationErrorMessage;
            HttpContext.Current.Session.Remove("ApplicationErrorMessage");
            return message;
        }

        /// <summary>
        /// Gets the message per page
        /// </summary>
        public static string GetAlertMessage(MessageType messageType, PageSessionName pageSessionName)
        {
            string sessionName = pageSessionName.ToString() + messageType.ToString();
            string message = string.Empty;
            if (HttpContext.Current.Session[sessionName] != null)
            {
                message = Convert.ToString(HttpContext.Current.Session[sessionName]);
                HttpContext.Current.Session[sessionName] = null;
            }
            return message;
        }

        /// <summary>
        /// Set the message per page
        /// </summary>
        public static void SetAlertMessage(MessageType messageType, PageSessionName pageSessionName, string message)
        {
            string sessionName = pageSessionName.ToString() + messageType.ToString();
            HttpContext.Current.Session[sessionName] = message;
        }

        /// <summary>
        /// Gets or sets the SiteName.
        /// </summary>
        public static string SiteName
        {
            get
            {
                return HttpContext.Current.Session["SiteName"] == null
                           ? string.Empty
                           : (string)HttpContext.Current.Session["SiteName"];
            }

            set
            {
                HttpContext.Current.Session["SiteName"] = value;
            }
        }
        
        /// <summary>
        /// Get Open popup message
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="pageSessionName"></param>
        /// <returns></returns>
        public static string GetSetOpenPopupMessage(MessageType messageType, PageSessionName pageSessionName)
        {
            string message = GetAlertMessage(messageType, pageSessionName);
            JAlertType jalertype = JAlertType.Alert;
            string messageHeader = Names.PromptSuccess;
            if (messageType == MessageType.Error || messageType == MessageType.HTMLError)
            {
                jalertype = JAlertType.Error;
                messageHeader = Names.PromptError;
            }

            string returnScript = string.Empty;
            if (!string.IsNullOrEmpty(message))
            {
                returnScript = ShowAlertMessage(message, messageHeader, jalertype.ToString());
            }
            return returnScript;
        }

        /// <summary>
        /// The show alert message.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ShowAlertMessage(string url, string message, string title, string alerType)
        {
            string cleanMessage = message.Replace("\\", "\\\\");
            cleanMessage = cleanMessage.Replace("'", "\\'");
            cleanMessage = cleanMessage.Replace("\r\n", " ");
            cleanMessage = cleanMessage.Replace("\r", " ");
            cleanMessage = cleanMessage.Replace("\n", " ");
            return @"<script type='text/javascript' language='javascript'>$(function() { showMessage('" + url + "',' " + cleanMessage + "',' " + title + "','" + alerType + "') ; })</script>";
        }

        /// <summary>
        /// The show alert message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ShowAlertMessage(string message, string title, string alerType)
        {
            string cleanMessage = message.Replace("\\", "\\\\");
            cleanMessage = cleanMessage.Replace("'", "\\'");
            cleanMessage = cleanMessage.Replace("\r\n", " ");
            cleanMessage = cleanMessage.Replace("\r", " ");
            cleanMessage = cleanMessage.Replace("\n", " ");
            var strString = @"<script type='text/javascript' language='javascript'>$(function() { showMessageOnly(' " + cleanMessage + "',' " + title + "','" + alerType + "') ; })</script>";
            return strString;
        }

        /// <summary>
        /// CustomJavascriptCall
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string CustomJavascriptCall(string message)
        {
            var strString = @"<script type='text/javascript' language='javascript'>" + message + "</script>";
            return strString;
        }
    }
}