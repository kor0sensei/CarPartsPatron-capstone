﻿using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CarPartsPatron.Models;

namespace CarPartsPatron.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(IConfiguration config) : base(config) { }
        public List<Car> GetAllCars()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, Manufacturer, Model, Submodel, Engine, Drivetrain, Transmission, Color, PhotoUrl FROM Cars";
                    var reader = cmd.ExecuteReader();

                    var cars = new List<Car>();

                    while (reader.Read())
                    {
                        cars.Add(new Car()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Submodel = reader.GetString(reader.GetOrdinal("Submodel")),
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
                       SELECT Cars.Id, Cars.UserProfileId, Cars.Manufacturer, Cars.Model, Cars.Submodel, Engine, Drivetrain, Transmission, Color, PhotoUrl 
                       FROM Cars
                       LEFT JOIN UserProfile ON Cars.UserProfileId = UserProfile.id
                       WHERE Cars.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Car car = new Car
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Submodel = reader.GetString(reader.GetOrdinal("Submodel")),
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
                        INSERT INTO Cars (UserProfileId, Manufacturer, Model, Submodel, Engine, DriveTrain, Transmission, Color, PhotoUrl)
                        OUTPUT INSERTED.ID
                        VALUES (@userProfileId, @manufacturer, @model, @subModel, @engine, @drivetrain, @transmission, @color, @photoUrl) ;";

                    cmd.Parameters.AddWithValue("@userProfileId", car.UserProfileId);
                    cmd.Parameters.AddWithValue("@manufacturer", car.Manufacturer);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@subModel", car.Submodel);
                    cmd.Parameters.AddWithValue("@engine", car.Engine);
                    cmd.Parameters.AddWithValue("@drivetrain", car.Drivetrain);
                    cmd.Parameters.AddWithValue("@transmission", car.Transmission);
                    cmd.Parameters.AddWithValue("@color", car.Color);
                    cmd.Parameters.AddWithValue("@photoUrl", car.PhotoUrl);
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    car.Id = id;
                }
            }
        }
        public void Delete(int carId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Cars
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", carId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Car car)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Cars
                            SET 
                                Manufacturer = @manufacturer,
                                Model = @model,
                                Submodel = @subModel,
                                Engine = @engine,
                                Drivetrain = @drivetrain,
                                Transmission = @transmission,
                                Color = @color,
                                PhotoUrl = @photoUrl
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@manufacturer", car.Manufacturer);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@subModel", car.Submodel);
                    cmd.Parameters.AddWithValue("@engine", car.Engine);
                    cmd.Parameters.AddWithValue("@drivetrain", car.Drivetrain);
                    cmd.Parameters.AddWithValue("@transmission", car.Transmission);
                    cmd.Parameters.AddWithValue("@color", car.Color);
                    cmd.Parameters.AddWithValue("@photoUrl", car.PhotoUrl);
                    cmd.Parameters.AddWithValue("@id", car.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}