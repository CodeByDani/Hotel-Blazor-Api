using System;
using System.ComponentModel.DataAnnotations;

namespace HiddenVilla_Server.Model
{
    public class HomeModel
    {
        [Required(ErrorMessage = "حتما وارد شود")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "حتما وارد شود")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        [Required(ErrorMessage = "حتما وارد شود")]
        public int NoOfNights { get; set; } = 1;
        [Required(ErrorMessage = "حتما وارد شود")]
        public int NoOfGuests { get; set; }

        public int Location { get; set; } = 0;
        public string AccommodationType { get; set; } = "";
    }
}