using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNRPS.POIMS.CommonResources.Models
{
    public class DataArchiveConfigurationModel
    {
        public int MaintenanceSettingKEY { get; set; }
        public int RetentionType { get; set; }
        public int RetentionValue { get; set; }
        public DateTime LastUpdatedTS { get; set; }
        public string Alarms { get; set; }
        public string HistoricalTrends { get; set; }
        public string WebBasedReports { get; set; }
        public string DataType { get; set; }
        public string DataRetentions { get; set; }


    }
}
