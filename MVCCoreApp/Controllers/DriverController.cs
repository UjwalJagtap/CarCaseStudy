using Microsoft.AspNetCore.Mvc;
using MVCCoreApp.Models;
using System.Linq;

namespace MVCCoreApp.Controllers
{
    public class DriverController : Controller
    {
        private readonly JourneyDbContext _context;

        public DriverController(JourneyDbContext context)
        {
            _context = context;
        }

        // **GET: Driver/Index (Display all drivers)**
        public IActionResult Index()
        {
            var drivers = _context.Drivers.ToList();  // Fetch all drivers from DriverDetails table
            if (drivers == null || !drivers.Any())
            {
                drivers = new List<Driver>();
            }
            return View(drivers);
        }

        // **GET: Driver/AddDriver**
        [HttpGet]
        public IActionResult AddDriver()
        {
            return View(new Driver());
        }

        // **POST: Driver/AddDriver**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDriver(Driver model)
        {
            if (ModelState.IsValid)
            {
                _context.Drivers.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "Driver added successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Failed to add driver.";
            return View(model);
        }

        // **GET: Driver/EditDriver**
        [HttpGet]
        public IActionResult EditDriver(int driverId)
        {
            var driver = _context.Drivers.Find(driverId);
            if (driver == null)
            {
                TempData["ErrorMessage"] = "Driver not found.";
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // **POST: Driver/EditDriver**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDriver(Driver model)
        {
            if (ModelState.IsValid)
            {
                var driver = _context.Drivers.Find(model.DriverId);
                if (driver != null)
                {
                    driver.DriverName = model.DriverName;
                    driver.MobileNo = model.MobileNo;
                    driver.Age = model.Age;
                    _context.SaveChanges();
                    TempData["Message"] = "Driver updated successfully!";
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = "Driver not found.";
            }
            return View(model);
        }

        // **POST: Driver/DeleteDriver**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDriver(int driverId)
        {
            var driver = _context.Drivers.Find(driverId);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                _context.SaveChanges();
                TempData["Message"] = "Driver deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Driver not found.";
            }
            return RedirectToAction("Index");
        }
    }
}
