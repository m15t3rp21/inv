// -----------------------------------------------------------------------
// <copyright file="ManualInputModel.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// 
namespace DNRPS.POIMS.Web.Models
{
    using System.ComponentModel.DataAnnotations;    
    using System.Collections.Generic;
    using DNRPS.POIMS.CommonResources;


    public class ManualInputModel
    {
        public int numberofTanks { get; set; }

        /// <summary>
        /// Gets or sets the property for lstSystemSite
        /// </summary>
        public List<int> lstSystemSite = new List<int>();

        public List<string> listTanks = new List<string>();

        public List<string> daftarTanks = new List<string>() {"T1", "T2", "T3", "T4" };
        
        public string userName { get; set; }

        public string TankName { get; set; }

        public string Level { get; set; }

        public string Temperature { get; set; }

        
    }
}