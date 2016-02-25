// -----------------------------------------------------------------------
// <copyright file="ManualInputModel.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// 
namespace DNRPS.POIMS.CommonResources.Models
{
    using System.ComponentModel.DataAnnotations;    
    using System.Collections.Generic;
    using DNRPS.POIMS.CommonResources;
    using DNRPS.POIMS.CommonResources.Enums;


    public class ManualInputModel
    {
        public int numberofTanks { get; set; }

        /// <summary>
        /// Gets or sets the property for lstSystemSite
        /// </summary>
        public List<int> lstSystemSite = new List<int>();

        public List<string> listTanks = new List<string>();

        public int TankKey { get; set; }

        public string TankName { get; set; }

        public double Level { get; set; }

        public double Temperature { get; set; }

        public string LevelUoMSymbol { get; set; }

        public string TemperatureUoMSymbol { get; set; }

        public string LevelSource { get; set; }

        public string TemperatureSource { get; set; }

        public ComputationModeType.EnumLiteral ComputationMode { get; set; }

        public bool SourceInput { get; set; }
        
    }
}