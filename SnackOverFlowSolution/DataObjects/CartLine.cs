using DataObjects;

namespace DataObjects
{
    /// <summary>
    /// Michael Takrama
    /// 
    /// Created:
    /// 2017/04/17
    /// 
    /// Cart Line Items used in Cart Class
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}