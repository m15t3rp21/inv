// -----------------------------------------------------------------------
// <copyright file="DataArchiveConfigurationController.cs" company="DNR Process Solutions Pte Ltd">
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


    public class DataArchiveConfigurationController : Controller
    {
        // GET: DataArchiveConfiguration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataRetentionPeriods()
        {
            DataArchiveConfigurationModel model = new DataArchiveConfigurationModel();

            return View(model);
        }



        public ActionResult TagsData_Read([DataSourceRequest]DataSourceRequest request)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }


        //// GET: DataArchiveConfiguration/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: DataArchiveConfiguration/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DataArchiveConfiguration/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DataArchiveConfiguration/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: DataArchiveConfiguration/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DataArchiveConfiguration/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DataArchiveConfiguration/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
