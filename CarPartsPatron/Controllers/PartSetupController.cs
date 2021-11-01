using Microsoft.AspNetCore.Mvc;
using CarPartsPatron.Models;
using CarPartsPatron.Repositories;
using System.Security.Claims;
using System;
using CarPartsPatron.Models.ViewModels;

namespace CarPartsPatron.Controllers
{
    public class PartSetupController : Controller
    {
        private readonly IPartSetupRepository _partSetupRepository;
        private readonly IPartRepository _partRepository;
        public PartSetupController(IPartSetupRepository partSetupRepository, IPartRepository partRepository)
        {
            _partSetupRepository = partSetupRepository;
            _partRepository = partRepository;
        }
        public ActionResult Index()
        {
            var partSetups = _partSetupRepository.GetAllPartSetups();
            return View(partSetups);
        }
        public ActionResult Create()
        {
            var parts = _partRepository.GetAllParts();
            var vm = new PartSetupCreateViewModel
            {
                PartOptions = parts
            };
            return View(vm);
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