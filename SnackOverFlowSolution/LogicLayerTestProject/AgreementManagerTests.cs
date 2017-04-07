using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Summary description for AgreementManagerTests
    /// Created by Mason Allen
    /// Created on 4/7/17
    /// </summary>
    [TestClass]
    public class AgreementManagerTests
    {
        private AgreementManager _agreementManager;
        private Agreement _agreement;

        [TestInitialize]
        public void TestInitialize()
        {
            _agreementManager = new AgreementManager();
            _agreement = null;
        }

        

        [TestCleanup]
        public void TestCleanUp()
        {
            _agreementManager = null;
            _agreement = null;

        }
    }
}
