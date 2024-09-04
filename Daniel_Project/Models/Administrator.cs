using System.ComponentModel.DataAnnotations;

namespace Daniel_Project.Models
{
    public class Administrator
    {
        [Key]
        public int? BookingID { get; set; }
        public string? FacilityDescription { get; set; }

        public string? FacilityDateFrom { get; set; }

        public string? FacilityDateTo { get; set; }

        public string? BookedBy { get; set; }

        public string? BookingStatus { get; set; }
    }
}
