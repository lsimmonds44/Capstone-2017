using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Summary description for ProductManagerTests
    /// Created by Mason Allen
    /// Created on 4/7/17
    /// </summary>
    [TestClass]
    public class ProductManagerTests
    {
        private ProductManager _productManager;
        private Product _product;

        [TestInitialize]
        public void TestInitialize()
        {
            _productManager = new ProductManager();
        }

        [TestMethod]
        public void TestCreateProductWithCorrectData()
        {
            //arange
            _product = new Product() { Active = true, DeliveryChargePerUnit = 20, Description = "A test product", Name = "Test Product", Price = 15, UnitOfMeasurement = "lbs", UnitPrice = 5, ImageBinary = new byte[1], ImageName = "Test Image"};
            int unexpectedResult = 0;

            //act
            int result = _productManager.CreateProduct(_product);

            //assert
            Assert.AreNotEqual(unexpectedResult, result);
            removeTestProductData();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            removeTestProductData();
        }

        public void removeTestProductData()
        {
            TestManager _testManager = new TestManager();
            _testManager.DeleteTestProduct();
        }
    }
}
