using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Status Manager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class StatusManagerTests
    {
        //declare private variables
        private StatusManager _statusManager;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _statusManager = new StatusManager();
        }

        [TestMethod]
        public void TestRetrieveStatusList()
        {
            //arrange
            //create new object
            List<string> statusList;
            //set expected result variable

            //act
            //execute method test, set to variable to test
            statusList = _statusManager.RetrieveStatusList();

            //assert
            //compare result to expected result
            Assert.IsTrue(statusList != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
        }
    }
}
