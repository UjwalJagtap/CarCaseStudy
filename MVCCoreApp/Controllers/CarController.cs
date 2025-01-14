using Microsoft.AspNetCore.Mvc;
using MVCCoreApp.Models;

namespace MVCCoreApp.Controllers
{
    public class CarController : Controller
    {
        private readonly JourneyDbContext _context;

        public CarController(JourneyDbContext context)
        {
            _context = context;
        }

        // **GET: Car/Index (Display all cars)**
        public IActionResult Index()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        // **GET: Car/AddCar (Render the add form)**
        [HttpGet]
        public IActionResult AddCar()
        {
            return View(new Car());
        }

        // **POST: Car/AddCar (Submit form data)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCar(Car model)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "Car added successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Failed to add car.";
            return View(model);
        }

        // **GET: Car/EditCar (Render edit form)**
        [HttpGet]
        public IActionResult EditCar(string carId)
        {
            var car = _context.Cars.Find(carId);  // Find car by ID
            if (car == null)
            {
                TempData["ErrorMessage"] = "Car not found.";
                return RedirectToAction("Index");
            }
            return View(car);  // Pass the car to the edit form
        }

        // **POST: Car/EditCar (Submit updated data)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCar(Car model)
        {
            if (ModelState.IsValid)
            {
                var car = _context.Cars.Find(model.CarId);
                if (car != null)
                {
                    car.CarModel = model.CarModel;
                    car.Color = model.Color;
                    car.TotalSeats = model.TotalSeats;
                    car.HasAC = model.HasAC;
                    _context.SaveChanges();  // Save the updated car details
                    TempData["Message"] = "Car updated successfully!";
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = "Car not found.";
            }
            return View(model);
        }
    }
}
