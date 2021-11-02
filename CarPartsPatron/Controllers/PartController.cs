using Microsoft.AspNetCore.Mvc;
using CarPartsPatron.Models;
using CarPartsPatron.Repositories;
using System.Security.Claims;
using System;
using CarPartsPatron.Models.ViewModels;

namespace CarPartsPatron.Controllers
{
    public class PartController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly ICarRepository _carRepository;
        public PartController(IPartRepository partRepository, ICarRepository carRepository)
        {
            _partRepository = partRepository;
            _carRepository = carRepository;
        }
        public ActionResult Index()
        {
            int userProfileId = GetCurrentUserProfileId();
            var parts = _partRepository.GetAllUserParts(userProfileId);

            return View(parts);
        }
        public ActionResult Create()
        {
            int userProfileId = GetCurrentUserProfileId();
            var partSetups = _carRepository.GetAllUserCars(userProfileId);
            var vm = new PartCreateViewModel
            {
                CarOptions = partSetups
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(Part part)
        {
            try
            {
                _partRepository.Add(part);

                return RedirectToAction("Index");
            }

            catch
            {
                return View(part);
            }
        }
        public ActionResult Edit(int id)
        {
            Part part = _partRepository.GetPartById(id);

            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        [HttpPost]
        public ActionResult Edit(int id, Part part)
        {
            try
            {
                _partRepository.Update(part);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(part);
            }
        }
        public ActionResult Delete(int id)
        {
            Part part = _partRepository.GetPartById(id);

            return View(part);
        }

        [HttpPost]
        public ActionResult Delete(int id, Part part)
        {
            try
            {
                _partRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(part);
            }
        }
        public int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
