using System;

namespace Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        private Product() {}

        public Product(string title, decimal price)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title must not be null");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price cannot be zero or negative");
            }

            Title = title;
            Price = price;
        }
        public override string ToString()
        {
            return Title;
        }
    }
}
