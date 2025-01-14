using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCoreApp.Models
{
    [Table("DriverDetails")]  // Maps this model to the DriverDetails table in the database
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; }  // Primary Key (matches with the DriverId column in the database)

        [Required(ErrorMessage = "Driver name is required")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [MaxLength(10), RegularExpression(@"[7-9]{1}[0-9]{9}", ErrorMessage = "Invalid mobile number")]
        public string MobileNo { get; set; }

        public int Age { get; set; }
    }
}
