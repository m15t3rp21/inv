// -----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace DNRPS.POIMS.Web.Controllers
{
    using Business;
    using CommonResources.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            // TODO: TEMP: Used for quick testing of DAL CRUD stuff for the time being...
            ////var tags = TagData.GetTagValues(new List<int> { 1 }, new List<EntityAttributeType.EnumLiteral> { EntityAttributeType.EnumLiteral.Tank_Average_Temperature_value, EntityAttributeType.EnumLiteral.Tank_Average_Temperature_status, EntityAttributeType.EnumLiteral.Tank_Level_value, EntityAttributeType.EnumLiteral.Tank_Level_status });
            ////var tank = TankConfig.DbRecords.Single(x => x.TankKey == 1);
            ////tank.RefreshLiveInventory(tags[1]);
            ////tank.UpdateLiveInventoryToDatabase();
            ////InventorySnapshot.UpdateLiveInventories(InventorySnapshot.GetLiveInventoryForTanks(new List<int> { 1, 2, 3, 4 }).Values);
            ////var dummyValue = new OpcUtility.OpcReadWriteResult (new OpcUtility.OpcDataPoint { OpcValue = DateTime.Now.Ticks % 50.1 })
            ////    { DoneAtLocal = DateTime.Now, IsSuccess = true, ResultMessage = "Testing Only" };
            ////TagData.UpdateTagValues(new Dictionary<string, OpcUtility.OpcReadWriteResult>
            ////{
            ////    { "-2147483647", dummyValue }
            ////});
            ////ViewBag.Message = string.Format("value : {0} , timestamp : {1}", dummyValue.OpcValue, dummyValue.DoneAtLocal);
            return View();
        }
    }
}
