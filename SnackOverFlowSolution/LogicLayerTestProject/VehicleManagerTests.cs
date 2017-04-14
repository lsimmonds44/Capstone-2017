using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Vehicle Manager Unit Tests
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class VehicleManagerTests
    {
        //declare private variables
        private VehicleManager _vehicleManager;
        private Vehicle testVehicle;
        int testVehicleId;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _vehicleManager = new VehicleManager();
        }

        [TestMethod]
        public void TestCreateVehicleWithCorrectData()
        {
            //arrange
            //create new object
            testVehicle = new Vehicle();
            testVehicle.Active = true;
            testVehicle.CheckedOut = false;
            testVehicle.Color = "White";
            testVehicle.Make = "Test Make";
            testVehicle.Model = "Test Model";
            testVehicle.Mileage = 123456;
            testVehicle.VehicleTypeID = "Truck";
            testVehicle.VIN = "123TEST456";
            testVehicle.Year = "2000";

            //set expected result variable

            //act
            //execute method test, set to variable to test
            testVehicleId = _vehicleManager.CreateVehicle(testVehicle);

            //assert
            //compare result to expected result
            Assert.IsTrue(testVehicleId > 0);
        }

        [TestMethod]
        public void TestCheckVehicleOutIn()
        {
            //arrange
            bool checkedOut = true;

            //act
            testVehicle = _vehicleManager.RetreiveVehicleById(testVehicleId);
            _vehicleManager.CheckVehicleOutIn(testVehicle);

            //assert
            Assert.IsTrue(checkedOut == testVehicle.CheckedOut);
        }

        [TestMethod]
        public void TestListAllVehicles()
        {
            //arrange
            List<Vehicle> testVehicleList;

            //act
            testVehicleList = _vehicleManager.RetrieveAllVehicles();

            //assert
            Assert.IsTrue(testVehicleList != null);
        }

        [TestMethod]
        public void TestRetrieveVehicleById()
        {
            //arrange

            //act
            testVehicle = _vehicleManager.RetreiveVehicleById(testVehicleId);

            //assert
            Assert.IsTrue(testVehicle != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
            if (testVehicleId != 0)
            {
                _vehicleManager.DeleteVehicle(testVehicleId);
            }
        }
    }
}
