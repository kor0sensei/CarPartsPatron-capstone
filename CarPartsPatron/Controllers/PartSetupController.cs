using Microsoft.AspNetCore.Mvc;
using CarPartsPatron.Models;
using CarPartsPatron.Repositories;
using System.Security.Claims;
using System;

namespace CarPartsPatron.Controllers
{
    public class PartSetupController : Controller
    {
        private readonly IPartSetupRepository _partSetupRepository;
        public PartSetupController(IPartSetupRepository partSetupRepository)
        {
            _partSetupRepository = partSetupRepository;
        }
        public ActionResult Index()
        {
            var partSetups = _partSetupRepository.GetAllPartSetups();
            return View(partSetups);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PartSetup partSetup)
        {
            try
            {
                _partSetupRepository.Add(partSetup);

                return RedirectToAction("Index");
            }

            catch
            {
                return View(partSetup);
            }
        }
        public ActionResult Edit(int id)
        {
            PartSetup partSetup = _partSetupRepository.GetPartSetupById(id);

            if (partSetup == null)
            {
                return NotFound();
            }

            return View(partSetup);
        }

        [HttpPost]
        public ActionResult Edit(int id, PartSetup partSetup)
        {
            try
            {
                _partSetupRepository.Update(partSetup);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(partSetup);
            }
        }
        public ActionResult Delete(int id)
        {
            PartSetup partSetup = _partSetupRepository.GetPartSetupById(id);

            return View(partSetup);
        }

        [HttpPost]
        public ActionResult Delete(int id, PartSetup partSetup)
        {
            try
            {
                _partSetupRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(partSetup);
            }
        }
    }
}