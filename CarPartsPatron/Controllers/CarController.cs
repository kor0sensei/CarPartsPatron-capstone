using Microsoft.AspNetCore.Mvc;
using CarPartsPatron.Models;
using CarPartsPatron.Repositories;

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
            var cars = _carRepository.GetAll();
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
            try
            {
                _carRepository.Add(car);

                return RedirectToAction("Index");
            }

            catch
            {
                return View(car);
            }
        }
    }
}
