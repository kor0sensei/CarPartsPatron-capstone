using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CarPartsPatron.Models;
using CarPartsPatron.Utils;

namespace CarPartsPatron.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(IConfiguration config) : base(config) { }
        public List<Car> GetAllUserCars(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Car.id, Year, Manufacturer, Model, Submodel, Engine, Drivetrain, Transmission, Color, PhotoUrl 
                                      FROM Car 
                                      JOIN UserProfile ON UserProfileId = UserProfile.id
                                      WHERE Car.UserProfileId = @userProfileId";

                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
                    var reader = cmd.ExecuteReader();

                    var cars = new List<Car>();

                    while (reader.Read())
                    {
                        cars.Add(new Car()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Year = reader.GetInt32(reader.GetOrdinal("Year")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Submodel = reader.GetString(reader.GetOrdinal("Submodel")),
                            Engine = reader.GetString(reader.GetOrdinal("Engine")),
                            Drivetrain = reader.GetString(reader.GetOrdinal("Drivetrain")),
                            Transmission = reader.GetString(reader.GetOrdinal("Transmission")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            PhotoUrl = DbUtils.GetNullableString(reader, ("PhotoUrl"))
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
                       SELECT Car.Id, Car.UserProfileId, Year, Manufacturer, Model, Submodel, Engine, Drivetrain, Transmission, Color, PhotoUrl 
                       FROM Car
                       LEFT JOIN UserProfile ON Car.UserProfileId = UserProfile.id
                       WHERE Car.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Car car = new Car
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Year = reader.GetInt32(reader.GetOrdinal("Year")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Submodel = reader.GetString(reader.GetOrdinal("Submodel")),
                            Engine = reader.GetString(reader.GetOrdinal("Engine")),
                            Drivetrain = reader.GetString(reader.GetOrdinal("Drivetrain")),
                            Transmission = reader.GetString(reader.GetOrdinal("Transmission")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            PhotoUrl = DbUtils.GetNullableString(reader, ("PhotoUrl")),
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
                        INSERT INTO Car (UserProfileId, Year, Manufacturer, Model, Submodel, Engine, DriveTrain, Transmission, Color, PhotoUrl)
                        OUTPUT INSERTED.ID
                        VALUES (@userProfileId, @year, @manufacturer, @model, @subModel, @engine, @drivetrain, @transmission, @color, @photoUrl) ;";

                    cmd.Parameters.AddWithValue("@userProfileId", car.UserProfileId);
                    cmd.Parameters.AddWithValue("@year", car.Year);
                    cmd.Parameters.AddWithValue("@manufacturer", car.Manufacturer);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@subModel", car.Submodel);
                    cmd.Parameters.AddWithValue("@engine", car.Engine);
                    cmd.Parameters.AddWithValue("@drivetrain", car.Drivetrain);
                    cmd.Parameters.AddWithValue("@transmission", car.Transmission);
                    cmd.Parameters.AddWithValue("@color", car.Color);
                    cmd.Parameters.AddWithValue("@photoUrl", DbUtils.ValueOrDBNull(car.PhotoUrl));
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
                            DELETE FROM Car
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
                            UPDATE Car
                            SET 
                                Year = @year,
                                Manufacturer = @manufacturer,
                                Model = @model,
                                Submodel = @subModel,
                                Engine = @engine,
                                Drivetrain = @drivetrain,
                                Transmission = @transmission,
                                Color = @color,
                                PhotoUrl = @photoUrl
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@year", car.Year);
                    cmd.Parameters.AddWithValue("@manufacturer", car.Manufacturer);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@subModel", car.Submodel);
                    cmd.Parameters.AddWithValue("@engine", car.Engine);
                    cmd.Parameters.AddWithValue("@drivetrain", car.Drivetrain);
                    cmd.Parameters.AddWithValue("@transmission", car.Transmission);
                    cmd.Parameters.AddWithValue("@color", car.Color);
                    cmd.Parameters.AddWithValue("@photoUrl", DbUtils.ValueOrDBNull(car.PhotoUrl));
                    cmd.Parameters.AddWithValue("@id", car.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}