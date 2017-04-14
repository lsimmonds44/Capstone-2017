using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;


namespace LogicLayerTestProject
{
    /// <summary>
    /// Vehicle Type Manager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class VehicleTypeManagerTests
    {
        //declare private variables
        VehicleTypeManager _vehicleTypeManager;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _vehicleTypeManager = new VehicleTypeManager();
        }

        [TestMethod]
        public void TestRetrieveVehicleTypeList()
        {
            //arrange
            //create new object
            //set expected result variable
            List<VehicleType> vehicleTypeList;

            //act
            //execute method test, set to variable to test
            vehicleTypeList = _vehicleTypeManager.retreiveVehicleTypeList();

            //assert
            //compare result to expected result
            Assert.IsTrue(vehicleTypeList != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
        }
    }
}
