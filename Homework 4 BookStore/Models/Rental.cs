using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_4_BookStore.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int PatronID { get; set; }
        public int BookID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }


        public Patron Patron { get; set; }
        public Book Book { get; set; }
    }

}
