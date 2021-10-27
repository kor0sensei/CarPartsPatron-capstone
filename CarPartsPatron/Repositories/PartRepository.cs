using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CarPartsPatron.Models;
using CarPartsPatron.Utils;

namespace CarPartsPatron.Repositories
{
    public class PartRepository : BaseRepository, IPartRepository
    {
        public PartRepository(IConfiguration config) : base(config) { }
        public List<Part> GetAllParts()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, CarId, Brand, PartType, Price, PhotoUrl, DateInstalled FROM Part";
                    var reader = cmd.ExecuteReader();

                    var parts = new List<Part>();

                    while (reader.Read())
                    {
                        parts.Add(new Part()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
                            Brand = reader.GetString(reader.GetOrdinal("Brand")),
                            PartType = reader.GetString(reader.GetOrdinal("PartType")),
                            Price = reader.GetInt32(reader.GetOrdinal("Price")),
                            PhotoUrl = reader.GetString(reader.GetOrdinal("PhotoUrl")),
                            DateInstalled = reader.GetDateTime(reader.GetOrdinal("DateInstalled"))
                        });
                    }

                    reader.Close();

                    return parts;
                }
            }
        }
        public Part GetPartById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Part.Id, Part.CarId, Brand, PartType, Price, Part.PhotoUrl, DateInstalled
                       FROM Part
                       LEFT JOIN Car ON Part.CarId = Car.id
                       WHERE Part.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Part part = new Part
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
                            Brand = reader.GetString(reader.GetOrdinal("Brand")),
                            PartType = reader.GetString(reader.GetOrdinal("PartType")),
                            Price = reader.GetInt32(reader.GetOrdinal("Price")),
                            PhotoUrl = reader.GetString(reader.GetOrdinal("PhotoUrl")),
                            DateInstalled = reader.GetDateTime(reader.GetOrdinal("DateInstalled")),
                        };


                        reader.Close();
                        return part;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
        public void Add(Part part)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Part (CarId, Brand, PartType, Price, DateInstalled, PhotoUrl)
                        OUTPUT INSERTED.ID
                        VALUES (@carId, @brand, @partType, @price, @dateInstalled, @photoUrl) ;";

                    cmd.Parameters.AddWithValue("@carId", part.CarId);
                    cmd.Parameters.AddWithValue("@brand", DbUtils.ValueOrDBNull(part.Brand));
                    cmd.Parameters.AddWithValue("@partType", part.PartType);
                    cmd.Parameters.AddWithValue("@price", DbUtils.ValueOrDBNull(part.Price));
                    cmd.Parameters.AddWithValue("@photoUrl", part.PhotoUrl);
                    cmd.Parameters.AddWithValue("@dateinstalled", DbUtils.ValueOrDBNull(part.DateInstalled));
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    part.Id = id;
                }
            }
        }
        public void Delete(int partId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Part
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", partId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Part part)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Part
                            SET 
                                CarId = @carId,
                                Brand = @brand,
                                PartType = @partType,
                                Price = @price,
                                PhotoUrl = @photoUrl,
                                DateInstalled = @dateInstalled
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@carId", part.CarId);
                    cmd.Parameters.AddWithValue("@brand", part.Brand);
                    cmd.Parameters.AddWithValue("@partType", part.PartType);
                    cmd.Parameters.AddWithValue("@price", part.Price);
                    cmd.Parameters.AddWithValue("@photoUrl", part.PhotoUrl);
                    cmd.Parameters.AddWithValue("@dateInstalled",part.DateInstalled);
                    cmd.Parameters.AddWithValue("@id", part.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
