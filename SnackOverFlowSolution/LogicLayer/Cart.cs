using System.Collections.Generic;
using System.Linq;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Michael Takrama
    /// 
    /// Created:
    /// 2017/04/17
    /// 
    /// Cart Logic
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
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