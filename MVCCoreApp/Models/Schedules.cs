using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCCoreApp.Models
{
    public class Schedules
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }

        [Required(ErrorMessage = "Journey Date must not be left empty")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime JourneyDate { get; set; }

        [ForeignKey("Car")]
        public string CarId { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public string StartLocation { get; set; }
        public string Destination { get; set; }

        [Required(ErrorMessage = "Total KM Driver must be specified")]
        public decimal TotalKmDriven { get; set; }

        [Required(ErrorMessage = "Rate per KM must be specified")]
        public int RatePerKm { get; set; }

        public virtual Car Car { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
