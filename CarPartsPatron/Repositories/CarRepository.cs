using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CarPartsPatron.Models;

namespace CarPartsPatron.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(IConfiguration config) : base(config) { }
        public List<Car> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, Manufacturer, Model, Submodel, Engine, Drivetrain, Transmission, Color, PhotoURL FROM Cars";
                    var reader = cmd.ExecuteReader();

                    var cars = new List<Car>();

                    while (reader.Read())
                    {
                        cars.Add(new Car()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Engine = reader.GetString(reader.GetOrdinal("Engine")),
                            Drivetrain = reader.GetString(reader.GetOrdinal("Drivetrain")),
                            Transmission = reader.GetString(reader.GetOrdinal("Transmission")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            PhotoUrl = reader.GetString(reader.GetOrdinal("PhotoUrl")),
                        });
                    }

                    reader.Close();

                    return cars;
                }
            }
        }
        public Car GetCarById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, Manufacturer, Model, Submodel, Engine, Drivetrain, Transmission, Color, PhotoURL 
                       FROM Cars
                       WHERE Car.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Car car = new Car
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Engine = reader.GetString(reader.GetOrdinal("Engine")),
                            Drivetrain = reader.GetString(reader.GetOrdinal("Drivetrain")),
                            Transmission = reader.GetString(reader.GetOrdinal("Transmission")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            PhotoUrl = reader.GetString(reader.GetOrdinal("PhotoUrl")),
                        };


                        reader.Close();
                        return car;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
        public void Add(Car car)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Cars (Manufacturer, Model, Submodel, Engine, DriveTrain, Transmission, Color)
                        OUTPUT INSERTED.ID
                        VALUES (@manufacturer, @model, @subModel, @engine, @drivetrain, @transmissionn, @color);";

                    cmd.Parameters.AddWithValue("@manufacturer", car.Manufacturer);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@subModel", car.Submodel);
                    cmd.Parameters.AddWithValue("@engine", car.Engine);
                    cmd.Parameters.AddWithValue("@drivetrain", car.Drivetrain);
                    cmd.Parameters.AddWithValue("@transmission", car.Transmission);
                    cmd.Parameters.AddWithValue("@manufacturer", car.Color);
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    car.Id = id;
                }
            }
        }
    }
}