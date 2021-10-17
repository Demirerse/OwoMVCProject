using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebUI.Models.ShoppingTools
{
    public static class ControlProduct
    {
        public static Cart KeepProduct(object sessionData)
        {
            Product bekleyenUrun = sessionData as Product;
            CartItem ci = new CartItem
            {
                ID = bekleyenUrun.ID,
                Name = bekleyenUrun.ProductName,
                ImagePath = bekleyenUrun.ImagePath,
                Price = Convert.ToDecimal(bekleyenUrun.UnitPrice)
            };
            Cart c = new Cart();
            c.SepeteEkle(ci);

            return c;
        }
    }
}