using Microsoft.EntityFrameworkCore;
using MVCCoreApp.Models;

namespace MVCCoreApp.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        JourneyDbContext context;
        public CarRepository(JourneyDbContext context)
        {
            this.context = context;
        }
        public bool Add(Car entity)
        {
            try
            {
                context.Cars.Add(entity);  // Add car to the DbSet
                int result = context.SaveChanges();  // Save changes to the database
                Console.WriteLine($"SaveChanges result: {result}");
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving: {ex.Message}");
                throw new Exception("Error while adding the car: " + ex.Message);
            }
        }



        public bool Delete(Car entity)
        {
            DbSet<Car> cars = context.Cars;
            cars.Remove(entity);
            int r = context.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Car> GetAll()
        {
            DbSet<Car> cars = context.Cars;
            return cars.ToList();
        }

        public Car Search(object id)
        {
            string carId = (string)id;
            DbSet<Car> cars = context.Cars;
            Car car = cars.Find(carId);
            return car;
        }

        public bool Update(Car entity)
        {
            DbSet<Car> cars = context.Cars;
            Car car = cars.Find(entity.CarId);
            car.CarModel = entity.CarModel;
            car.Color = entity.Color;
            car.HasAC = entity.HasAC;
            car.TotalSeats = entity.TotalSeats;
            int r = context.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}