using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// CategoryManager Unit Test Class
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class CategoryManagerTests
    {
        //declare private variables
        private CategoryManager _categoryManager;
        private Category testCategory;
        bool success;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _categoryManager = new CategoryManager();
        }

        [TestMethod]
        public void TestCreateCategory()
        {
            //arrange
            //create new object
            testCategory = new Category();
            testCategory.CategoryID = "Test Category Id";
            testCategory.Description = "Test Category";
            //set expected result variable

            //act
            //execute method test, set to variable to test
            success = _categoryManager.CreateCategory(testCategory);

            //assert
            //compare result to expected result
            Assert.IsTrue(success);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
            _categoryManager.DeleteCategory("Test Category Id");
        }
    }
}
