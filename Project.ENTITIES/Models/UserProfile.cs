using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class UserProfile:BaseEntity
    {
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur"), Display(Name = "İsim")]
        //[MaxLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur"),  Display(Name = "Soyisim")]
        //[MaxLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir.")]
        public string LastName { get; set; }

        public string Address { get; set; }




        // Relational Properties 

        public virtual AppUser AppUser { get; set; }
    }
}
