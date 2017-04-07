using System.Collections.Generic;
using System.Linq;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Created by Michael Takrama
    /// 04/07/2017
    /// 
    /// Cart Logic
    /// </summary>
    public class Cart
    {
        private readonly List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddItem(Product product, int quantity)
        {
            var line = lineCollection
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (line == null)
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            else line.Quantity += quantity;
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}