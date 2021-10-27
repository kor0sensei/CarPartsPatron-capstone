using System.Collections.Generic;
using CarPartsPatron.Models;

namespace CarPartsPatron.Repositories
{
    public interface IPartRepository
    {
        List<Part> GetAllParts();
        Part GetPartById(int id);
        void Add(Part part);
        void Update(Part part);
        void Delete(int Id);
    }
}
