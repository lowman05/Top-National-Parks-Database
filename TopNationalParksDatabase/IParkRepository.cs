using TopNationalParksDatabase.Models;

namespace TopNationalParksDatabase
{
    public interface IParkRepository
    {
        public IEnumerable<Park> GetAllParks();
        public Park GetPark(int id);
        public void UpdatePark(Park park);

        public void InsertPark(Park parkToInsert);

        public void DeletePark(Park park);

        public IEnumerable<Park> Gallery();

        public Park GetPreviousPark(int id);

        public Park GetNextPark(int id);
    }
}
