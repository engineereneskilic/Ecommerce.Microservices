using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
   
    public class OrderItem : Entity
    {
        public string ProductID { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public decimal Price { get; private set; }

        public int OrderId { get; private set; } // foreign key
        public Order Order { get; private set; } // navigation property

        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductID = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        private OrderItem() { } // EF Core için


    }
}
