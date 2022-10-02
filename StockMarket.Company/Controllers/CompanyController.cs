using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Company.Authentication;
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
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public CompanyController(ICompanyService companyService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this._companyService = companyService;
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [HttpGet]
        public string Hello()
        {
            return "Hello from CompanyController API Service";
        }

        [HttpPost("register")]
        public ActionResult Register(CompanyDetailsRequest company)
        {
            try
            {
                return Ok(_companyService.RegisterCompany(company));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("info")]
        public ActionResult GetCompanyDetails(string companyCode)
        {
            try
            {
                return Ok(_companyService.GetCompanyDetails(companyCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("getall")]
        public ActionResult GetCompanyDetails()
        {
            try
            {
                return Ok(_companyService.GetAllCompanyDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public ActionResult DeleteCompany(string companyCode)
        {
            try
            {
                return Ok(_companyService.DeleteCompanyDetails(companyCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
        [HttpPost("adduser")]
        public ActionResult AddUser(User user)
        {
            try
            {
                return Ok(_companyService.AddUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("getuser")]
        public ActionResult GetUser()
        {
            try
            {
                return Ok(_companyService.GetUser());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [AllowAnonymous]
        [HttpGet("Authenticate")]
        public IActionResult GetAuthentication(LoginModel loginModel)
        {
            var token = _jwtAuthenticationManager.Authenticate(loginModel.Email, loginModel.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                loginModel.Token = token;
                return Ok(loginModel);
            }
        }
    }
}
