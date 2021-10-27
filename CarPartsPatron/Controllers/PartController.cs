using Microsoft.AspNetCore.Mvc;
using CarPartsPatron.Models;
using CarPartsPatron.Repositories;
using System.Security.Claims;
using System;

namespace CarPartsPatron.Controllers
{
    public class PartController : Controller
    {
        private readonly IPartRepository _partRepository;
        public PartController(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }
        public ActionResult Index()
        {
            var parts = _partRepository.GetAllParts();
            return View(parts);
        }
        public ActionResult Create()
        {
            return View();
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
    }
}
