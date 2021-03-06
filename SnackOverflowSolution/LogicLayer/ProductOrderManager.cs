﻿using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{

    public class ProductOrderManager : IProductOrderManager
    {
        public List<ProductOrder> RetrieveProductOrdersByStatus(String Status)
        {
            List<ProductOrder> ProductOrderList;
            try
            {
                ProductOrderList = ProductOrderAccessor.RetrieveProductOrdersByStatus(Status);
                foreach (ProductOrder ProductOrderFound in ProductOrderList)
                {
                    ProductOrderFound.BackorderPreorderList = BackorderPreorderAccessor.RetrieveBackorderPreorders(
                        new BackorderPreorder
                        {
                            OrderId = ProductOrderFound.OrderId
                        }
                    );

                    ProductOrderFound.DeliveryList = DeliveryAccessor.RetrieveDelivery(
                        new Delivery
                        {
                            OrderId = ProductOrderFound.OrderId
                        }
                    );

                    ProductOrderFound.EmployeeOrderResponsibilityList = EmployeeOrderResponsibilityAccessor.RetrieveEmployeeOrderResponsibility(
                        new EmployeeOrderResponsibility
                        {
                            OrderId = ProductOrderFound.OrderId
                        }
                    );

                    ProductOrderFound.PackageList = PackageAccessor.RetrievePackageFromSearch(
                        new Package
                        {
                            OrderId = ProductOrderFound.OrderId
                        }
                    );
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProductOrderList;
		}





        /// <summary>
        /// Victor Algarin
        /// Created 2017/02/08
        /// 
        /// Retrieves the details for a specific order through an order id
        /// </summary>
        public ProductOrder retrieveProductOrderDetails(int orderID)
        {
		ProductOrder order = null;
            try
            {
                order = ProductOrderAccessor.RetrieveProductOrder(orderID);
            }
            catch (Exception)
            {

                throw new ApplicationException("There was a problem retrieving the product order details.");
            }

            return order;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/16
        /// 
        /// updates the status of the order
        /// </summary>
        /// <param name="productOrderID">The ProductOrderID of the order to be updated</param>
        /// <param name="status">The status to assign to the order</param>
        /// <returns>bool representing whether the update was successful</returns>
        public bool UpdateProductOrderStatus(int productOrderID, string status)
        {
            bool result = false;

            try
            {
                if (ProductOrderAccessor.UpdateProductOrderStatus(productOrderID, status) > 0)
                {
                    result = true;
                }

            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Eric Walton 
        /// 2017/9/3
        /// Invokes a method in the product order accessor to create an order
        /// </summary>
        /// <param name="productOrder"></param>
        /// <returns>The new order id that is auto generated by the database.</returns>
        public int createProductOrder(ProductOrder productOrder)
        {
            int result = 0;
            try
            {
               result = ProductOrderAccessor.CreateProductOrder(productOrder);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }



        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/23
        /// </summary>
        /// <param name="oldOrder"></param>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public bool UpdateProductOrder(ProductOrder oldOrder, ProductOrder newOrder)
        {
            bool result = false;

            try
            {
                if (ProductOrderAccessor.UpdateProductOrder(oldOrder, newOrder) > 0)
                {
                    result = true;
                }

            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// William Flood
        /// Created on 2017/04/27
        /// </summary>
        /// <param name="orderID"></param>
        public int SaveOrder(int orderID)
        {
            try
            {
                return ProductOrderAccessor.SaveOrder(orderID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured: ", ex);
            }
        }


        /// <summary>
        /// William Flood
        /// Created on 2017/04/27
        /// </summary>
        /// <param name="productOrderID"></param>
        public int LoadOrder(int productOrderID)
        {
            try
            {
                return ProductOrderAccessor.LoadOrder(productOrderID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured: ", ex);
            }
        }

        /// <summary>
        /// William Flood
        /// Created on 2017/04/27
        /// </summary>
        /// <param name="username"></param>
        public List<int> RetrieveSaveOrders(string username)
        {
            try
            {
                return ProductOrderAccessor.RetrieveSaveOrders(username);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured: ", ex);
            }
        }
    }
}
