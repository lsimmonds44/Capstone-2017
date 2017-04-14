using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Delivery Type Manager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class DeliveryTypeManagerTests
    {
        //declare private variables
        private DeliveryTypeManager _deliveryTypeManager;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _deliveryTypeManager = new DeliveryTypeManager();
        }

        [TestMethod]
        public void TestRetrieveDeliveryTypeList()
        {
            //arrange
            //create new object
            //set expected result variable
            List<string> deliveryTypeList;

            //act
            //execute method test, set to variable to test
            deliveryTypeList = _deliveryTypeManager.RetrieveDeliveryTypeList();

            //assert
            //compare result to expected result
            Assert.IsTrue(deliveryTypeList != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
        }
    }
}
