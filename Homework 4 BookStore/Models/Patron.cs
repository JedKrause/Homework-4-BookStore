using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;

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
        [Display(Name = "Permissions Level")]
        [Required]
        public int PermissionsLevel { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName
        {
            get { return LastName + ", " + FirstName; }

        }

        //public Patron GetPatron(string _username, string _password)
        //{

        //    using (var cn = new SqlConnection(@"Server=(localdb)\\mssqllocaldb;Database=Homework_4_BookStore;Trusted_Connection=True;MultipleActiveResultSets=true"))
        //    {
        //        string _sql = @"SELECT * FROM [dbo].[Patron] " +
        //               @"WHERE [LastName] = @u AND [Password] = @p";
        //        var cmd = new SqlCommand(_sql, cn);
        //        cmd.Parameters
        //            .Add(new SqlParameter("@u", SqlDbType.NVarChar))
        //            .Value = _username;
        //        cmd.Parameters
        //            .Add(new SqlParameter("@p", SqlDbType.NVarChar))
        //            .Value = _password;
        //        cn.Open();
        //        var reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            Patron patron = new Patron();
        //            patron.PatronID = reader.GetInt32(0);
        //            patron.FirstName = reader.GetString(1);
        //            patron.LastName = reader.GetString(2);
        //            patron.PermissionsLevel = reader.GetInt32(5);

        //            cmd.Dispose();
        //            return patron;
        //        }
        //        else
        //        {
        //            Patron empty = new Patron();
        //            reader.Dispose();
        //            cmd.Dispose();
        //            return empty;
        //        }
        //    }
        //}
        public ICollection<Rental> Rentals { get; set; }
    }
}
