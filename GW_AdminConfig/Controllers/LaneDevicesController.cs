using GW_Business.Models;
using GW_Communication.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GW_Business.Models;
using GW_Communication.DB;
using GW_AdminConfig.Models;
using System.Security.AccessControl;
using GW_Communication.DB;

//using GW_Util.Log;
//using GW_Util.Settings;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using GW_AdminConfig.DB;
using Humanizer.Configuration;
using System.Drawing.Drawing2D;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Rendering;
using GW_AdminConfig.Repository.Interface;
using GW_Util.Log;

namespace GW_AdminConfig.Controllers
{
    public class LaneDevicesController : Controller
    {
        private readonly ILaneDevices _laneDevices;
        public LaneDevicesController(ILaneDevices laneDevices)
        {
            _laneDevices = laneDevices;
        }
        public ActionResult Index()
        {
            IEnumerable<LaneDeviceModel> objLaneDeviceModel = new List<LaneDeviceModel>();
            try
            {
                objLaneDeviceModel = _laneDevices.GetAllLaneDevices();
                ViewBag.Lanes = new SelectList(objLaneDeviceModel.Select(x => x.LaneId).Distinct().ToList());
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
            return View(objLaneDeviceModel);
        }

        // GET: LaneDevicesController/Details/5
        public ActionResult Details(int DeviceId)
        {
            IEnumerable<LaneDeviceModel> objLaneDeviceModel = _laneDevices.GetLaneDevicesById(DeviceId);
            return View(objLaneDeviceModel);
        }

        // GET: LaneDevicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LaneDevicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaneDevicesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LaneDevicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaneDevicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LaneDevicesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
