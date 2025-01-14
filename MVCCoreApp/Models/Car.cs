using System.ComponentModel.DataAnnotations;

namespace MVCCoreApp.Models
{
    public class Car
    {
        [Key]  // Primary key attribute
        public string CarId { get; set; }

        public string CarModel { get; set; }
        public string Color { get; set; }
        public int TotalSeats { get; set; }
        public bool HasAC { get; set; }
    }
}
