using NUnit.Framework;
using StockMarket.Company.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    [TestFixture]
    public class StockTest
    {
        string companyCode = "AMB";
        [Test]
        public void CheckStock()
        {
            StockDetails stockDetails = new StockDetails() { CompanyCode= companyCode };
            Assert.AreEqual(companyCode, stockDetails.CompanyCode);
        }
    }
}
