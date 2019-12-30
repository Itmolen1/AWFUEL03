﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IT.Core.ViewModels;
using IT.Repository.WebServices;

namespace IT.Web.Controllers
{
    public class DirectSaleController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverModel> driverModels = new List<DriverModel>();
        public ActionResult Index()
        {
            return View ();
        }

        public ActionResult DirectSaleCreate()
        {
           return View ();
        }

        [HttpPost]
        public JsonResult VehicleByTrafficPlateNumber(DriverModel driverModel)
        {
            try
            {

                var DriverViewList = webServices.Post(driverModel, "Vehicle/VehicleByTrafficPlateNumber");
                if (DriverViewList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    driverModels = (new JavaScriptSerializer().Deserialize<List<DriverModel>>(DriverViewList.Data.ToString()));

                    return Json(driverModels, JsonRequestBehavior.AllowGet);
                }

                return Json("failed",JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(DriverModel driverModel)
        {
            try
            {
                var DriverViewModelList = webServices.Post(driverModel, "Vehicle/DirectSaleVehicleAndDriverAdd");
                if (DriverViewModelList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    driverModel = (new JavaScriptSerializer().Deserialize<DriverModel>(DriverViewModelList.Data.ToString()));
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}