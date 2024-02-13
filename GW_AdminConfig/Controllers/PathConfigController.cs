using GW_AdminConfig.DB;
using GW_AdminConfig.Models;
using GW_AdminConfig.Repository.Interface;
using GW_AdminConfig.Repository.Service;
using GW_Business.Models;
using GW_Communication.DB;
using GW_Communication.Models;
using GW_Util.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace GW_AdminConfig.Controllers
{
    public class PathConfigController : Controller
    {
        private readonly IPathConfigSettings _pathConfigSettings;
        private readonly IConfiguration _configuration;
        public PathConfigController(IPathConfigSettings pathConfigSettings, IConfiguration configuration)
        {
            _pathConfigSettings = pathConfigSettings;
            _configuration = configuration;
        }

        // GET: PathConfigController
        public ActionResult Index()
        {
            IEnumerable<PathConfigModel> objLaneDeviceModel = new List<PathConfigModel>();
            int? page = 5;
            try
            {
                objLaneDeviceModel = _pathConfigSettings.GetAllSettings("General");
              //  ViewBag.Category = new SelectList(objLaneDeviceModel.Select(x => x.Category).Distinct().ToList());

                int pageSize = 3; // Set the number of items per page
                int pageNumber = (page ?? 5); // Get the page number from the query string or default to 1

                var paginatedList = objLaneDeviceModel.ToPagedList(pageSize, pageNumber);
            }
            catch (Exception Ex)
            {
                Logging.LogError(Ex);
                throw;
            }
            return View(objLaneDeviceModel);
        }

        [HttpGet]
        public ActionResult SearchSettings(string search = "")
        {
            DateTime startTime = DateTime.Now;
            List<PathConfigModel> pathConfigModel = new List<PathConfigModel>();
            var searchResult = pathConfigModel.Where(x => x.Category == search).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                return RedirectToAction("Index", searchResult);
            }
            return RedirectToAction("Index", searchResult);
        }
        // GET: PathConfigController/Details/5
        public ActionResult Details(string Category, string Name)
        {
            IEnumerable<PathConfigModel> objLaneDeviceModel = _pathConfigSettings.GetSettingsByParams(Category, Name);
            return View(objLaneDeviceModel);
        }

        // GET: PathConfigController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PathConfigController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                PathConfigModel pathConfigModel = new PathConfigModel
                {
                    Category = Convert.ToString(collection["Category"]),
                    Name = Convert.ToString(collection["Name"]),
                    Value = Convert.ToString(collection["Value"]),
                    ExtraField1= Convert.ToString(collection["ExtraField1"]),
                    ExtraField2= Convert.ToString(collection["ExtraField2"]),
                    LastUpdatedBy= Convert.ToString(collection["LastUpdatedBy"])
                };
                int rowsAffected = _pathConfigSettings.AddConfigSettings(pathConfigModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PathConfigController/Edit/5


        [HttpPost("PathConfig/Edit")]
        public IActionResult Edit(string Category, string Name)
        {
            DateTime startTime = DateTime.Now;
            PathConfigModel? pathConfigModel = new PathConfigModel();
            LaneDevicesLogic objLaneDevicesLogic = new LaneDevicesLogic(_configuration);

            var entity = _pathConfigSettings.GetSettingsByParams(Category, Name);
            pathConfigModel = entity.FirstOrDefault(x => x.Category == Category);           

            return PartialView("_EditModal", pathConfigModel);
        }

        // POST: PathConfigController/Edit/5
        [HttpPost("PathConfig/EditSetting")]

        public ActionResult EditSetting(IFormCollection collection)
        {
            try
            {
                PathConfigModel pathConfigModel = new PathConfigModel
                {
                    Category = Convert.ToString(collection["Category"]),
                    Name = Convert.ToString(collection["Name"]),
                    Value = Convert.ToString(collection["Value"]),
                    ExtraField1 = Convert.ToString(collection["ExtraField1"]),
                    ExtraField2 = Convert.ToString(collection["ExtraField2"]),
                    LastUpdatedBy = Convert.ToString(collection["LastUpdatedBy"])
                };
                int rowsAffected = _pathConfigSettings.UpdateSettings(pathConfigModel);
                if (rowsAffected < 0)
                {
                    TempData["Success"] = "Updated Successfully!";
                }
                else
                {
                    TempData["Success"] = "Something went wrong please try again leter.";
                }
             
               
            }
            catch
            {
                TempData["Success"] = "Something went wrong please try again leter.";
            }

            return RedirectToAction("Index", "PathConfig");
        }

        // GET: PathConfigController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PathConfigController/Delete/5
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
