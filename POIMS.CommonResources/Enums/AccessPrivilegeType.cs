// -----------------------------------------------------------------------
// <copyright file="AccessPrivilegeType.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.CommonResources.Enums
{
    using System;
    using System.Collections.Generic;

    public class AccessPrivilegeType
    {
        public static List<int> GetAllAccessPrivilegeTypes()
        {
            var allPrivilegesArray = Enum.GetValues(typeof(AccessPrivilegeType.EnumLiteral));
            var allPrivilegesList = new List<int>(allPrivilegesArray.Length);
            foreach (AccessPrivilegeType.EnumLiteral item in allPrivilegesArray)
            {
                allPrivilegesList.Add((int)item);
            }

            return allPrivilegesList;
        }

        public static string Print(EnumLiteral printMe)
        {
            return Names.ResourceManager.GetString(string.Format("AccessPrivilegeType{0}", printMe));
        }

        public enum EnumLiteral
        {
            ConfigureTanks = 9001,
            ConfigureProducts = 9002,
            ConfigureTankGroups = 9003,
            ConfigureProductCategories = 9004,
            ViewReports = 9005,
            OperateControlEquipments = 9006,
            UpdateInputInventoryValues = 9007,
            ShutDownSystems = 9008,
            ConfigureInputMode = 9009,
            AcknowledgeAlarms = 9010,
            AcknowledgeAllAlarms = 9011,
            DeActivateKeyBlocker = 9012,
            ClearAuditLogs = 9013,
            ManageDB = 9014,
            ServerAccess = 9015,
            HMIAccess = 9016,
            ManageCustomField = 9017,
            ViewCustomField = 9018,
            // TODO: implement other new POIMS privileges
        }
    }
}
