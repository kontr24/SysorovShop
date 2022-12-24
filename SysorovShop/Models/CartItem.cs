using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysorovShop.Models
{
    public class CartItem

    {
        public CartItem()
        {

        }

        //public CartItem(ShoppingDetails shoppingDetails)
        //{
        //    Quantity = 1;
        //    //Sum = cartItem.Product.Price.ToString();
        //}

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}




