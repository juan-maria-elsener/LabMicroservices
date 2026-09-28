using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class ProductItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        protected ProductItem() { }

        public ProductItem(string name, string description, decimal price, int stockQuantity)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public void DeductStock(int quantity)
        {
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Stock insuficiente");

            StockQuantity -= quantity;
        }

        public void AddStock(int quantity)
        {
            StockQuantity += quantity;
        }

    }
}
