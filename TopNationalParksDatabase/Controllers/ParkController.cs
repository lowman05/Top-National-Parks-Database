
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using System.Net.Http.Headers;
using System.Text.Json;
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
           
           return View (parks);
        }


        public IActionResult ViewPark(int id)
        {
            
            var park = repo.GetPark(id);
            var previousParkId = repo.GetPreviousPark(id)?.ParkID;
            var nextParkId = repo.GetNextPark(id)?.ParkID;
                    var parkViewModel = new ParkViewModel
            {
                Park = park,
                PreviousParkId = previousParkId,
                NextParkId = nextParkId
            };
            IEnumerable<Park> alerts = repo.GetAlertsByParkCode();
            IEnumerable<Park> parkAlerts = alerts.Where(a => a.ParkCode !=null && a.ParkCode == park.ParkCode);
            List<Park> parkAlertsList = parkAlerts.ToList();
            parkViewModel.Alerts = parkAlertsList;


            return View(parkViewModel);
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

        public IActionResult Gallery()
        {
            var photos = repo.Gallery();
            return View(photos);
        }







    }
}
