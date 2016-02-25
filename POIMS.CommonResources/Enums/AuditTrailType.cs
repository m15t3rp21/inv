// -----------------------------------------------------------------------
// <copyright file="AuditTrailType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    public class AuditTrailType
    {
        public struct Category
        {
            // TODO: implement POIMS category
            //// CHEM-INV
            //public const string MANAGE_PRODUCTION = "Update Production";
            //public const string MANAGE_CHEMICAL = "Manage Chemicals";
            //public const string MANAGE_ORDER = "Manage Replenishment Orders";
        }

        public struct LogType
        {
            public const string ERROR = "Error";
            public const string INFORMATION = "Information";
            public const string WARNING = "Warning";
        }

        public struct Severity
        {
            public const string CRITICAL = "Critical";
            public const string LOW = "Low";
            public const string MEDIUM = "Medium";
            public const string UNSPECIFIED = "Unspecified";
        }
    }
}
