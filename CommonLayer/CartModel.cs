using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BookInCart { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string Author { get; set; }
        public double DiscountPrice { get; set; }
        public double ActualPrice { get; set; }
        public int BookQuantity { get; set; }
    }
}
