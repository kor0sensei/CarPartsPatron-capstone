using System.Collections.Generic;
using CarPartsPatron.Models;


namespace CarPartsPatron.Repositories
{
    public interface ICarRepository
    {
        List<Car> GetAllUserCars(int id);
        Car GetCarById(int id);
        void Add(Car car);
        void Update(Car car);
        void Delete(int Id);
    }
}
