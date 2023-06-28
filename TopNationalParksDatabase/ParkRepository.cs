using Dapper;
using System.Data;
using TopNationalParksDatabase.Models;

namespace TopNationalParksDatabase
{
    public class ParkRepository : IParkRepository
    {
        private readonly IDbConnection _conn;
        public ParkRepository(IDbConnection conn) 
        {
            _conn = conn;
        }

        public IEnumerable<Park> GetAllParks()
        {
            return _conn.Query<Park>("SELECT * FROM PARKS;");
        }

        public Park GetPark(int id)
        {
            return _conn.QuerySingle<Park>("SELECT * FROM PARKS WHERE PARKID = @id", new { id = id });
        }

        public void UpdatePark(Park park)
        {
            _conn.Execute("UPDATE parks SET FullName = @fullname, ParkCode = @parkcode, PhoneNumber = @phonenumber, EmailAddress = @emailaddress, Website = @website WHERE ParkID = @id",
                new { fullname = park.FullName, parkcode = park.ParkCode, phonenumber = park.PhoneNumber, emailaddress = park.EmailAddress, website = park.Website, id = park.ParkID });
        }

        public void InsertPark(Park parkToInsert)
        {
            _conn.Execute("INSERT INTO parks (FULLNAME, PARKCODE, PHONENUMBER, EMAILADDRESS, WEBSITE) VALUES(@fullname, @parkcode, @phonenumber, @emailaddress, @website);",
                new { fullname = parkToInsert.FullName, parkcode = parkToInsert.ParkCode, phonenumber = parkToInsert.PhoneNumber, emailaddress = parkToInsert.EmailAddress, website = parkToInsert.Website, });
        }

        public void DeletePark(Park park)
        {
            _conn.Execute("DELETE FROM parks WHERE ParkID = @id;", new {id = park.ParkID});
        }
    }
}
