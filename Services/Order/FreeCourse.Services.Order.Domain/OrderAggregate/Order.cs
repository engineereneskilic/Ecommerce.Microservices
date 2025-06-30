using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    // EF Core Features
    // Owned Types (Address)
    // Backing Field (_orderItems)
    // Parametresiz ctor EF Core için
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }

        public Address Address { get; private set; }

        public string BuyerID { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // Ana constructor – iş mantığı için
        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerID = buyerId;
            Address = address;
        }

        // EF Core için parametresiz constructor
        private Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            if (_orderItems.Any(x => x.ProductID == productId)) return;

            var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
            _orderItems.Add(newOrderItem);
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
