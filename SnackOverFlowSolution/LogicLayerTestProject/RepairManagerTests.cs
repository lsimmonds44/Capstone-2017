using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Repair Manager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class RepairManagerTests
    {
        //declare private variables
        private RepairManager _repairManager;
        private VehicleManager _vehicleManager;
        private Vehicle testVehicle;
        int testVehicleId;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _repairManager = new RepairManager();
            _vehicleManager = new VehicleManager();
        }

        [TestMethod]
        public void TestRetrieveRepairListByVehicleId()
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
            List<Repair> repairListbyVehicleId;

            //act
            //execute method test, set to variable to test
            testVehicleId = _vehicleManager.CreateVehicle(testVehicle);
            repairListbyVehicleId = _repairManager.RetreiveAllRepairsForVehicle(testVehicleId);

            //assert
            //compare result to expected result
            Assert.IsTrue(repairListbyVehicleId != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
            _vehicleManager.DeleteVehicle(testVehicleId);
        }
    }
}
