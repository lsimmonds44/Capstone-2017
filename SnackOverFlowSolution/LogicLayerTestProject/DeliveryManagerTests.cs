using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Created By Dan Brown
    /// 04/21/17
    /// 
    /// 
    /// </summary>
    [TestClass]
    public class DeliveryManagerTests
    {

        private DeliveryManager _deliveryManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _deliveryManager = new DeliveryManager();
        }

        [TestMethod]
        public void TestRetrieveDeliveries()
        {

        }


    }
}
