using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Company.DTO.Request;
using StockMarket.Company.DTO.Response;
using StockMarket.Company.Models;
using StockMarket.Company.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.Controllers
{  
   
    [Route("api/v1.0/market/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet]
        public string Hello()
        {
            return "Hello from CompanyController API Service";
        }

        [HttpPost("register")]
        public void Register(CompanyDetailsRequest company)
        {
            var result= _companyService.RegisterCompany(company);
        }

        [HttpGet("info")]
        public ActionResult<List<CompanyDetails>> GetCompanyDetails(string companyCode)
        { 
            var result= _companyService.GetCompanyDetails(companyCode);
            return result;
        }

        [HttpGet("getall")]
        public ActionResult<List<CompanyDetails>> GetCompanyDetails()
        {
            var result = _companyService.GetAllCompanyDetails();
            return result;
        }

        [HttpPost("delete")]
        public void DeleteCompany(string companyCode)
        {
            var result = _companyService.DeleteCompanyDetails(companyCode);
        }

        //[HttpPost("AddStock")]
        //public void AddStockPrice(StockDetailsRequest stockDetailsRequest)
        //{
        //    var result = _companyService.AddStockDetails(stockDetailsRequest);
        //}

        //[HttpGet("getAllStockDetails")]
        //public ActionResult<List<StockDetails>> GetAllStockDetails()
        //{
        //    var result = _companyService.GetAllStockDetails();
        //    return result;
        //}

        //[HttpPost("DeleteStock")]
        //public void DeleteStock(string id)
        //{
        //    var result = _companyService.DeleteStockDetails(id);
        //}
    }
}
