using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingApp.Models
{
    public class AddToCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public string UserName { get; set; }
        public int ProductPrice { get; set; }
        public int Quntity { get; set; }

    }
}
