using TopNationalParksDatabase.Models;

namespace TopNationalParksDatabase
{
    public interface IReviewRepository
    {
        public IEnumerable<Review>GetAllReviews();
        public void AddReview(Review reviewToInsert);
        public Review AssignPark();

        public IEnumerable<Park> GetAllParks();
    }
}
