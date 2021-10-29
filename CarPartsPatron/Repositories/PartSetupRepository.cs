using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CarPartsPatron.Models;
using CarPartsPatron.Utils;

namespace CarPartsPatron.Repositories
{
    public class PartSetupRepository : BaseRepository, IPartSetupRepository
    {
        public PartSetupRepository(IConfiguration config) : base(config) { }
        public List<PartSetup> GetAllPartSetups()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT PartSetup.id, PartId, SetupNote, CreateDateTime, Part.PartType FROM PartSetup 
                                       JOIN Part ON PartSetup.PartId = Part.Id";
                    var reader = cmd.ExecuteReader();

                    var partSetups = new List<PartSetup>();

                    while (reader.Read())
                    {
                        partSetups.Add(new PartSetup()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PartId = reader.GetInt32(reader.GetOrdinal("partId")),
                            SetupNote = reader.GetString(reader.GetOrdinal("SetupNote")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            Part = new Part
                            {
                                PartType = reader.GetString(reader.GetOrdinal("PartType"))
                            }
                        });
                    }

                    reader.Close();

                    return partSetups;
                }
            }
        }
        public PartSetup GetPartSetupById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT PartSetup.Id, PartId, SetupNote, CreateDateTime
                       FROM PartSetup
                       LEFT JOIN Part 
                       ON PartSetup.PartId = Part.id
                       WHERE PartId = Part.id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        PartSetup partSetup = new PartSetup
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PartId = reader.GetInt32(reader.GetOrdinal("PartId")),
                            SetupNote = reader.GetString(reader.GetOrdinal("SetupNote")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("PartId")))
                        {
                            partSetup.Part = new Part
                            {
                                PartType = reader.GetString(reader.GetOrdinal("PartType"))
                            };
                        }


                        reader.Close();
                        return partSetup;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
        public void Add(PartSetup partSetup)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO PartSetup (PartId, SetupNote, CreateDateTime)
                        OUTPUT INSERTED.ID
                        VALUES (@partId, @setupNote, GETDATE())";

                    cmd.Parameters.AddWithValue("@partId", partSetup.PartId);
                    cmd.Parameters.AddWithValue("@setupNote", partSetup.SetupNote);
                    ;

                    int id = (int)cmd.ExecuteScalar();
                    partSetup.Id = id;
                }
            }
        }
        public void Update(PartSetup partSetup)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE PartSetup
                            SET 
                                PartId = @partId,
                                SetupNote = @setupNote,
                                CreateDateTime = @createDateTime
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@carId", partSetup.PartId);
                    cmd.Parameters.AddWithValue("@setupNote", partSetup.SetupNote);
                    cmd.Parameters.AddWithValue("@createDateTime", partSetup.CreateDateTime);
                    cmd.Parameters.AddWithValue("@id", partSetup.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int partSetupId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM PartSetup
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", partSetupId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}