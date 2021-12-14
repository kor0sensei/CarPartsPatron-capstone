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
            int userProfileId = GetCurrentUserProfileId();
            var partSetups = _partSetupRepository.GetAllUserPartSetups(userProfileId);

            return View(partSetups);
        }
        public ActionResult Create()
        {
            int userProfileId = GetCurrentUserProfileId();
            var parts = _partRepository.GetAllUserParts(userProfileId);
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
        public int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}