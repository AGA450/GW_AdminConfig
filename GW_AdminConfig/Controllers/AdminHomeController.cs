﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GW_Business.Models;
using GW_Communication.DB;


namespace GW_AdminConfig.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHomeController
        public ActionResult Index()
        {
            try
            {
               
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        // GET: AdminHomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminHomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminHomeController/Create
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

        // GET: AdminHomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminHomeController/Edit/5
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

        // GET: AdminHomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminHomeController/Delete/5
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
