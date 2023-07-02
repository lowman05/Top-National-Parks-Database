using Dapper;
using System.Data;
using System.Net.Http.Headers;
using TopNationalParksDatabase.Models;

namespace TopNationalParksDatabase
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IDbConnection _conn;

        public ReviewRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Review> GetAllReviews()
        {
            string query = @"
        SELECT r.*, p.FullName AS ParkName
        FROM reviews r
        JOIN parks p ON r.ParkID = p.ParkID";

            return _conn.Query<Review>(query);
        }
        public void AddReview(Review reviewToInsert)
        {
             _conn.Execute("INSERT INTO reviews (PARKID, REVIEWER, RATING, COMMENTS, DATE) VALUES (@parkid, @reviewer, @rating, @comments, @date);",
                new { parkid = reviewToInsert.ParkID, reviewer = reviewToInsert.Reviewer, rating = reviewToInsert.Rating, comments = reviewToInsert.Comments, date = reviewToInsert.Date });
        }
        public Review AssignPark()
        {
            var parkList = GetAllParks();
            var review = new Review();
            review.Parks = parkList;
            return review;
        }

        public IEnumerable<Park> GetAllParks()
        {
            return _conn.Query<Park>("SELECT * FROM parks");
        }


    }
}

