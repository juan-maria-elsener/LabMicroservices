using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class OrderEntity
    {
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int CustomerId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public decimal TotalAmount { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        protected OrderEntity() { }

        public OrderEntity(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            OrderDate = DateTime.UtcNow;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, int quantity)
        {
            var item = new OrderItem(productId, productName, unitPrice, quantity);
            _items.Add(item);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.Subtotal);
        }
    }

}
