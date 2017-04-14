using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// WarehouseManager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class WarehouseManagerTests
    {
        //declare private variables
        private WarehouseManager _warehouseManager;
        private Warehouse testWarehouse;
        int testWarehouseId = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _warehouseManager = new WarehouseManager();
        }

        [TestMethod]
        public void TestCreateWarehouseWithCorrectData()
        {
            //arrange
                //create new object
            testWarehouse = new Warehouse();
            testWarehouse.AddressOne = "123 Test Street";
            testWarehouse.AddressTwo = "Building 1";
            testWarehouse.City = "Test City";
            testWarehouse.State = "IA";
            testWarehouse.Zip = "12345";
                //set expected result variable
            
            //act
                //execute method test, set to variable to test
            testWarehouseId = _warehouseManager.addWarehouse(testWarehouse);

            //assert
                //compare result to expected result
            Assert.IsTrue(testWarehouseId > 0);
        }

        [TestMethod]
        public void TestListWarehouses()
        {
            //arrange
            List<Warehouse> warehouseList;

            //act
            warehouseList = _warehouseManager.ListWarehouses();

            //assert
            Assert.IsTrue(warehouseList != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (testWarehouseId != 0)
            {
                _warehouseManager.deleteWarehouse(testWarehouseId);
            }
        }

    }
}
