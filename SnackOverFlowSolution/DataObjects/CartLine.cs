using DataObjects;

namespace DataObjects
{
    /// <summary>
    /// Created by Michael Takrama
    /// 04/07/2017
    /// 
    /// Cart Line Items used in Cart Class
    /// </summary>
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}