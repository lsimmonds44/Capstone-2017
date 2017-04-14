using System;
using System.Diagnostics;
using System.Linq;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class CustomerOrderManager : ICustomerOrderManager
    {
        /// <summary>
        ///     Created by Michael Takrama
        ///     04/13/17
        /// 
        ///     Processes Orders from the MVC Layer
        /// </summary>
        /// <param name="cart">Cart object containing OrderLine Items</param>
        /// <param name="shippingDetails">Contains shipping Details</param>
        public bool ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            var orderId = 0;

            // Order Creation in Product Order
            try
            {
                var p = new ProductOrder
                {
                    CustomerId = shippingDetails.CustomerId,
                    OrderDate = DateTime.Now,
                    Discount = 0, // defaulted to 0 % until discount functionality added -- temporaire
                    UserAddressId = 1
                };

                orderId = ProductOrderAccessor.CreateOrder(p);

                if (orderId == 0)
                {
                    Debug.WriteLine("CustomerOrderManager: Error Creating order");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new ApplicationException("CustomerOrderManager: " + e.Message);
            }

            // Writing Order Lines
            try
            {
                if (cart.Lines.Select(o => new OrderLine
                    {
                        ProductOrderID = orderId,
                        ProductID = o.Product.ProductId,
                        Quantity = o.Quantity,
                        GradeID = o.Product.GradeId,
                        Price = (decimal)o.Product.Price,
                        UnitDiscount = 0 //temporaire
                    }).All( lineToWrite => 1 == OrderLineAccessor.CreateOrderLine(lineToWrite) )
                )
                {
                    Debug.WriteLine("CustomerOrderManager: Error during order line writing");
                    // delete created order entry
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            //Write shipping details to user address. //USER ADDRESS TABLE
            try
            {
                // lookup user id of customer
                // update user address in user table
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