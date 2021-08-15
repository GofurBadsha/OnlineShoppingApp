using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingApp.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; }
        public int ProductPrice { get; set; }
        public int TotalAmount { get; set; }
        public string ProductQty { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNo { get; set; }
        public string UserAdress { get; set; }

    }
}
