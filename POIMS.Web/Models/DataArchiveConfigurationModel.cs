// -----------------------------------------------------------------------
// <copyright file="DataArchiveConfigurationModel.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// 
namespace DNRPS.POIMS.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using DNRPS.POIMS.CommonResources;
    using System;

    public class DataArchiveConfigurationModel
    {
        public int MaintenanceSettingKEY { get; set; }
        public int RetentionType { get; set; }
        public int RetentionValue { get; set; }
        public DateTime LastUpdatedTS { get; set; }
        public string Alarms { get; set; }
        public string HistorcalTrends { get; set; }
        public string WebBasedReports { get; set; }
        public string DataType { get; set; }
        public string DataRetentions { get; set; }

        
    }
}