
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
    using DNRPS.POIMS.Workflow;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;


    public class ManualInputController : Controller
    {
        // GET: ManualInput
        public ActionResult ManualInput()
        {
            return View(new ManualInputModel());
        }

        public ActionResult TanksData_Read([DataSourceRequest]DataSourceRequest request)
        {

            var data = DNRPS.POIMS.Workflow.Services.ManualInputModelService.Read().ToDataSourceResult(request);

            return Json(data);
        }


        public ActionResult TanksConfigurationData_Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = DNRPS.POIMS.Workflow.Services.ManualInputModelService.Read().ToDataSourceResult(request);

            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TankConfig> tanks)
        {
            if (tanks != null && ModelState.IsValid)
            {
                ////foreach (var tank in tanks)
                ////{

                ////}
            }

            return Json(tanks.ToDataSourceResult(request, ModelState));
        }
        /// 
        /// Manual Input page
        /// 
        public ActionResult InputConfiguration()
        {

            return View();
        }
    }

}