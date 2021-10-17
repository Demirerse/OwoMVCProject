using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser:BaseEntity
    {
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur"), Display(Name = "Kullanıcı İsmi")]
        //[MaxLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir."), MinLength(2, ErrorMessage = "Minimum {1} karakter girebilirsiniz")]
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        public Guid ActivationCode { get; set; }
        public bool Active { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Email alanını boş geçilemez"), EmailAddress(ErrorMessage = "Geçersiz Email formatı")]
        public string Email { get; set; }
       

        [Required(ErrorMessage = "Şifre alanı boş geçilemez."), Display(Name = "Şifre")]
        public string Password { get; set; }


        //[Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez.")]
        //[Compare("Password", ErrorMessage = "Girilen şifreler eşleşmiyor."), Display(Name = "Şifre Tekrar")]
        public string ConfirmPassword { get; set; }

        public AppUser()
        {
            Role = UserRole.Member;
            ActivationCode = Guid.NewGuid();
        }

        // Relational Properties 
      
        public virtual UserProfile UserProfile { get; set; }
        public virtual List<Order> Orders { get; set; }


    }
}
