using Microsoft.AspNetCore.Mvc;
using MVCCoreApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCoreApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly JourneyDbContext _context;

        public ScheduleController(JourneyDbContext context)
        {
            _context = context;
        }

        // **GET: Schedule/Index (Display all schedules)**
        public IActionResult Index()
        {
            var schedules = _context.Schedules
                                     .Include(s => s.Car)
                                     .Include(s => s.Driver)
                                     .ToList();
            return View(schedules);
        }

        // **GET: Schedule/AddSchedule (Display form to add schedule)**
        [HttpGet]
        public IActionResult AddSchedule()
        {
            // Retrieve the list of cars and drivers from the database
            var cars = _context.Cars.ToList();
            var drivers = _context.Drivers.ToList();

            if (cars == null || drivers == null || !cars.Any() || !drivers.Any())
            {
                TempData["ErrorMessage"] = "No cars or drivers available. Please add them first.";
                return RedirectToAction("Index"); // Redirect to Schedule Index if lists are empty
            }

            // Pass lists to the view using ViewBag
            ViewBag.Cars = new SelectList(cars, "CarId", "CarModel");
            ViewBag.Drivers = new SelectList(drivers, "DriverId", "DriverName");

            return View();
        }

        // POST: Add Schedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSchedule(Schedules model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Save the new schedule to the database
                    _context.Schedules.Add(model);
                    _context.SaveChanges();  // Save changes in the database
                    TempData["SuccessMessage"] = "Schedule added successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error while saving schedule: " + ex.Message;
                    return View(model);  // Return view with error message
                }
            }

            // If model validation fails, reload dropdowns and show the form again
            ViewBag.Cars = new SelectList(_context.Cars.ToList(), "CarId", "CarModel");
            ViewBag.Drivers = new SelectList(_context.Drivers.ToList(), "DriverId", "DriverName");
            return View(model);
        }



        // **GET: Schedule/EditSchedule/{id} (Display form to edit schedule)**
        [HttpGet]
        public IActionResult EditSchedule(int id)
        {
            var schedule = _context.Schedules.Find(id);
            if (schedule == null)
            {
                TempData["ErrorMessage"] = "Schedule not found.";
                return RedirectToAction("Index");
            }
            ViewBag.Cars = _context.Cars.ToList();
            ViewBag.Drivers = _context.Drivers.ToList();
            return View(schedule);
        }

        // **POST: Schedule/EditSchedule/{id} (Update schedule)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSchedule(Schedules schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Schedules.Update(schedule);
                _context.SaveChanges();
                TempData["Message"] = "Schedule updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Cars = _context.Cars.ToList();
            ViewBag.Drivers = _context.Drivers.ToList();
            return View(schedule);
        }

        // **POST: Schedule/DeleteSchedule/{id} (Delete schedule)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSchedule(int id)
        {
            var schedule = _context.Schedules.Find(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                _context.SaveChanges();
                TempData["Message"] = "Schedule deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Schedule not found.";
            }
            return RedirectToAction("Index");
        }

        // **GET: Schedule/Details/{id} (View detailed schedule info)**
        [HttpGet]
        public IActionResult Details(int id)
        {
            var schedule = _context.Schedules
                                    .Include(s => s.Car)
                                    .Include(s => s.Driver)
                                    .FirstOrDefault(s => s.ScheduleId == id);
            if (schedule == null)
            {
                TempData["ErrorMessage"] = "Schedule not found.";
                return RedirectToAction("Index");
            }

            // Calculate total fare
            ViewBag.TotalFare = schedule.TotalKmDriven * schedule.RatePerKm;
            return View(schedule);
        }
    }
}
