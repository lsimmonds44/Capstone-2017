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
        private readonly List<CartLine> _lineCollection = new List<CartLine>();


        public IEnumerable<CartLine> Lines
        {
            get { return _lineCollection; }
        }

        public void AddItem(Product product, int quantity)
        {
            var line = _lineCollection
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (line == null)
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            else line.Quantity += quantity;
        }

        public void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public double ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }
    }
}