using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product:BaseEntity
    {
        [Required(ErrorMessage = "Ürün isim alanı boş geçilemez."), Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        //[Range(1, 5000, ErrorMessage = "{0} sadece {1} ve {2} aralığında fiyat kabul edebilir")]
        [Display(Name = "Ürün fiyatı")]
        public decimal UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public int? CategoryID { get; set; }


        //Relational Properties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
