using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_4_BookStore.Models
{
    public class ShoppingCart
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int PatronID { get; set; }
        [Required]
        public int BookID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Qty { get; set; }
        public string Path { get; set; }
    }
}
