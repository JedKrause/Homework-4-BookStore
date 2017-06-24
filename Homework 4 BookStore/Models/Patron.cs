using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_4_BookStore.Models
{
    public class Patron
    {
        public int PatronID { get; set; }
        [StringLength(30)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(30)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        [Display(Name ="Membership Date")]
        public DateTime MembershipDate { get; set; }
        public string FullName
        {
            get { return LastName + ", " + FirstName; }

        }

        public ICollection<Rental> Rentals { get; set; }
    }
}
