using Microsoft.AspNetCore.Mvc;
using TopNationalParksDatabase.Models;

namespace TopNationalParksDatabase.Controllers
{
    public class ParkController : Controller
    {
        private readonly IParkRepository repo;
        
        public ParkController(IParkRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            var parks = repo.GetAllParks();
            return View(parks);
        }

        public IActionResult ViewPark (int id)
        {
            var park = repo.GetPark(id);
            return View(park);
        }

        public IActionResult UpdatePark(int id)
        {
           Park park = repo.GetPark(id);
            if (park == null)
            {
                return View("ParkNotFound");
            }
            return View(park);
        }

        public IActionResult UpdateParkToDatabase(Park park)
        {
            repo.UpdatePark(park);
            return RedirectToAction("ViewPark", new {id = park.ParkID});
        }

       public IActionResult InsertPark()
        {
            var park = new Park();
            return View(park);
        }

        public IActionResult InsertParkToDatabase(Park parkToInsert)
        {
            repo.InsertPark(parkToInsert);
            return RedirectToAction("Index");
        }

        public IActionResult DeletePark(Park park)
        {
            repo.DeletePark(park);
            return RedirectToAction("Index");
        }
    }
}
