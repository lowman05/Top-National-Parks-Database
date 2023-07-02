using Microsoft.AspNetCore.Mvc;
using TopNationalParksDatabase.Models;
namespace TopNationalParksDatabase.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository repo;
        public ReviewController(IReviewRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(string sortBy = "rating", string sortOrder = "asc")
        {
            var reviews = repo.GetAllReviews();
            switch (sortBy)
            {
                case "rating":
                    if (sortOrder == "asc")
                        reviews = reviews.OrderBy(r => r.Rating);
                    else
                        reviews = reviews.OrderByDescending(r => r.Rating);
                    break;
                case "date":
                    if (sortOrder == "asc")
                        reviews = reviews.OrderBy(r => r.Date);
                    else
                        reviews = reviews.OrderByDescending(r => r.Date);
                    break;
                case "park name":
                    if (sortOrder == "asc")
                        reviews = reviews.OrderBy(r => r.ParkName);
                    else
                        reviews = reviews.OrderByDescending(r => r.ParkName);
                    break;
                default:
                    break;

            }
            return View(reviews);
        }

        public IActionResult AddReview()
        {
            var review = repo.AssignPark();
            return View(review);
        }

        public IActionResult AddReviewToDatabase(Review reviewToInsert)
        {
            repo.AddReview(reviewToInsert);
            return RedirectToAction("Index");
        }
       
    }
}
