using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Created by Michael Takrmaa
    /// 04/07/2017
    /// 
    /// Interface for Customer Orders on MVC Layer
    /// </summary>
    public interface IOrderManager
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}