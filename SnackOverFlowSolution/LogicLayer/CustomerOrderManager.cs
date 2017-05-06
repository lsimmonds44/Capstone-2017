using System;
using System.Diagnostics;
using System.Linq;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class CustomerOrderManager : ICustomerOrderManager
    {
        private readonly UserManager userManager = new UserManager();
        private readonly CustomerManager customerManager = new CustomerManager();


        /// <summary>
        ///     Created by Michael Takrama
        ///     04/13/17
        ///     
        ///     Processes Orders from the MVC Layer
        ///     William Flood
        ///     2017/04/29
        ///     Updated method to use cart from the database
        /// 
        /// </summary>
        /// <param name="cart">Cart object containing OrderLine Items</param>
        /// <param name="shippingDetails">Contains shipping Details</param>
        public int ProcessOrder(ShippingDetails shippingDetails)
        {
            var user = userManager.RetrieveUserByUserName(shippingDetails.IdentityUsername);
            var customer = customerManager.RetrieveCommercialCustomerByUserId(user.UserId);
            int orderId = 0;
            try
            {
                var p = new ProductOrder
                {
                    CustomerId = customer.UserId, 
                    OrderTypeId = "Commercial", 
                    AddressType = null,
                    OrderDate = DateTime.Now,
                    Address1 = shippingDetails.Line1 + "," + shippingDetails.Line2,
                    City = shippingDetails.City,
                    State = shippingDetails.State,
                    Zip = shippingDetails.Zip,
                    OrderStatusId = "Open",
                    HasArrived = false,
                    Discount = 0, // defaulted to 0 % until discount functionality added -- temporaire
                };

                orderId = ProductOrderAccessorMvc.CreateProductOrder(p);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ApplicationException("CustomerOrderManager: " + e.Message);
            }



            return orderId;
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 04/28/2017
        /// 
        /// Submits order lines for Order
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private static bool SubmitOrderLines(Cart cart, int orderId)
        {
            try
            {
                bool all = cart.Lines.Select(o => new OrderLine
                {
                    ProductOrderID = orderId,
                    ProductName = o.Product.Name,
                    ProductID = o.Product.ProductId,
                    Quantity = o.Quantity,
                    GradeID = o.Product.GradeId,
                    Price = (decimal) o.Product.Price,
                    UnitDiscount = 0 //temporaire
                }).All(lineToWrite => 0 < OrderLineAccessor.CreateOrderLine(lineToWrite));

                if (!all)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            return true;
        }
    }
}