using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;

namespace LogicLayerTestProject
{
    [TestClass]
    public class CustomerManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {

        }


        [TestMethod]
        public void TestCreateCommercialAccount()
        {
            //Arrange
            CommercialCustomer testCustomer = new CommercialCustomer() { UserId = 99999,  };

            //Act


            //Assert
        }


    }
}
