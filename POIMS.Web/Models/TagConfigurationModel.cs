// -----------------------------------------------------------------------
// <copyright file="TagConfigurationModel.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// 
namespace DNRPS.POIMS.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using DNRPS.POIMS.CommonResources;

    public class TagConfigurationModel
    {
        public int TagKey { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public int RefEntityKey { get; set; }
    }
}