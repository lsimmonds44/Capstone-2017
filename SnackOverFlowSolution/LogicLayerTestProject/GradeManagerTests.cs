using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Grade Manager Unit Tests
    /// Created by Mason Allen
    /// Created on 4/13/17
    /// </summary>
    [TestClass]
    public class GradeManagerTests
    {
        //declare private variables
        private GradeManager _gradeManager;
        string[] gradeList;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate private variables
            _gradeManager = new GradeManager();
        }

        [TestMethod]
        public void TestRetrieveGradeList()
        {
            //arrange
            //create new object
            //set expected result variable

            //act
            //execute method test, set to variable to test
            gradeList = _gradeManager.RetrieveGradeList().ToArray();


            //assert
            //compare result to expected result
            Assert.IsTrue(gradeList != null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //remove test data
        }
    }
}
