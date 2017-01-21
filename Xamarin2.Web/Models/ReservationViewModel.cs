using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xamarin2.Web.Models
{
    public class ReservationViewModel
    {
        public DateTime Date { get; set; }

        public string DateString { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public int PhoneNumber { get; set; }

        public string PhoneNumberString { get; set; }

        [Display(Name = "Number of people")]
        public int NumberOfPeople { get; set; }

        public List<int> Tables { get; set; }
    }
}