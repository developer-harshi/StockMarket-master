using Moq;
using NUnit.Framework;
using StockMarket.Company.Models;
using StockMarket.Company.Repository.Service;
using StockMarket.Company.Services.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    [TestFixture]
    public class CompanyTest
    {
        string email = "priyanka.ziraff@gmail.com";
        string companyCode = "AMB";
        [Test]
        public void CheckUser()
        {
            User user = new User() { Email = email };
            Assert.AreEqual(email, user.Email);
        }
        [Test]
        public void Checkdetails()
        {
            CompanyDetails companyDetails = new CompanyDetails() {CompanyCode=companyCode };
            Assert.AreEqual(companyCode, companyDetails.CompanyCode);
        }
    }
}
