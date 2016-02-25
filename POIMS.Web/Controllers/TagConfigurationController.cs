// -----------------------------------------------------------------------
// <copyright file="TagConfiguratorController.cs" company="DNR Process Solutions Pte Ltd">
//  Copyright© DNR Process Solutions Pte Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace DNRPS.POIMS.Web.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using DNRPS.POIMS.Business;
    using DNRPS.POIMS.CommonResources;
    using DNRPS.POIMS.CommonResources.Enums;
    using DNRPS.POIMS.CommonResources.Models;
    using DNRPS.POIMS.Web.Models;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    public class TagConfigurationController : BaseController
    {
        // GET: TagConfigurator
        public ActionResult Index()
        {
            return View(new TagConfigurationModel());
        }

        public ActionResult TagsData_Read([DataSourceRequest]DataSourceRequest request)
        {
            var query = TankConfig.DbRecords.Select(m => new TagConfigurationModel {
                TagKey = m.TankKey,
                TagDescription = m.PlantDescription,
                TagName = m.Key.ToString(),
            });
            var data = query.ToDataSourceResult(request);
            return Json(data);
            ////return View();
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }


        ////// GET: TagConfigurator/Details/5
        ////public ActionResult Details(int id)
        ////{
        ////    return View();
        ////}

        ////// GET: TagConfigurator/Create
        ////public ActionResult Create()
        ////{
        ////    return View();
        ////}

        ////// POST: TagConfigurator/Create
        ////[HttpPost]
        ////public ActionResult Create(FormCollection collection)
        ////{
        ////    try
        ////    {
        ////        // TODO: Add insert logic here

        ////        return RedirectToAction("Index");
        ////    }
        ////    catch
        ////    {
        ////        return View();
        ////    }
        ////}

        ////// GET: TagConfigurator/Edit/5
        ////public ActionResult Edit(int id)
        ////{
        ////    return View();
        ////}

        ////// POST: TagConfigurator/Edit/5
        ////[HttpPost]
        ////public ActionResult Edit(int id, FormCollection collection)
        ////{
        ////    try
        ////    {
        ////        // TODO: Add update logic here

        ////        return RedirectToAction("Index");
        ////    }
        ////    catch
        ////    {
        ////        return View();
        ////    }
        ////}

        ////// GET: TagConfigurator/Delete/5
        ////public ActionResult Delete(int id)
        ////{
        ////    return View();
        ////}

        ////// POST: TagConfigurator/Delete/5
        ////[HttpPost]
        ////public ActionResult Delete(int id, FormCollection collection)
        ////{
        ////    try
        ////    {
        ////        // TODO: Add delete logic here

        ////        return RedirectToAction("Index");
        ////    }
        ////    catch
        ////    {
        ////        return View();
        ////    }
        ////}
    }
}
