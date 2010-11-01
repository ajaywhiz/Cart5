using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CartFive.Utils;

namespace CartFive.Models
{

    public class Customer
    {
        public string Id { get; set; }

        [Required(ErrorMessage="Customer Name is required.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage="Shipping Address is required.")]
        public string Address { get; set; }        

        [Required(ErrorMessage="Email is required.")]
        [EmailDataType(ErrorMessage = "Email is invalid.")]        
        public string Email { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
                
        public short Qty { get; set; }
        
        public decimal Price { get; set; }
    }

    public class Order
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        public DateTime OrderDate { get; set; }

        public Customer CustomerInfo { get; set; }

        public List<OrderItem> OrderItems { get; set; }


        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PayableAmount { get; set; }

    }
}