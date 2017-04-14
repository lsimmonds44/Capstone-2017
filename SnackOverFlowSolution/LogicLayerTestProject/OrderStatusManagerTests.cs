using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Order Status Manager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class OrderStatusManagerTests
    {
        //declare private variables
        private OrderStatusManager _orderStatusManager;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _orderStatusManager = new OrderStatusManager();
        }

        [TestMethod]
        public void TestRetrieveOrderStatusList()
        {
            //arrange
            //create new object
            //set expected result variable
            List<string> orderStatusList;

            //act
            //execute method test, set to variable to test
            orderStatusList = _orderStatusManager.RetrieveAllOrderStatus();

            //assert
            //compare result to expected result
            Assert.IsTrue(orderStatusList != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
        }
    }
}
