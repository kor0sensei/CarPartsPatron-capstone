using System.Collections.Generic;
using CarPartsPatron.Models;

namespace CarPartsPatron.Repositories
{
    public interface IPartSetupRepository
    {
        List<PartSetup> GetAllPartSetups();
        List<PartSetup> GetAllUserPartSetups(int id);
        PartSetup GetPartSetupById(int id);
        void Add(PartSetup partSetup);
        void Update(PartSetup partSetup);
        void Delete(int Id);
    }
}
