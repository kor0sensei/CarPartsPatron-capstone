using Microsoft.AspNetCore.Mvc;
using CarPartsPatron.Models;
using CarPartsPatron.Repositories;
using System.Security.Claims;
using System;

namespace CarPartsPatron.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        public CarController( ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public ActionResult Index()
        {
            int userProfileId = GetCurrentUserProfileId();
            var cars = _carRepository.GetAllUserCars(userProfileId);

            return View(cars);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Car car)
        {
            car.UserProfileId = GetCurrentUserProfileId();
            try
            {
                _carRepository.Add(car);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return View(car);
            }
        }

        public ActionResult Delete(int id)
        {
            Car car = _carRepository.GetCarById(id);

            return View(car);
        }

        [HttpPost]
        public ActionResult Delete(int id, Car car)
        {
            try
            {
                _carRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(car);
            }
        }
        public int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
        public ActionResult Edit(int id)
        {
            Car car = _carRepository.GetCarById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                _carRepository.Update(car);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(car);
            }
        }
    }
}
