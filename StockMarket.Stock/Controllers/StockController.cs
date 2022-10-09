using Microsoft.AspNetCore.Mvc;
using StockMarket.Stock.DTO.Request;
using StockMarket.Stock.Models;
using StockMarket.Stock.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.Controllers
{
   
    [Route("api/v1.0/market/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet]
        public string Hello()
        {
            return "Hello from StockController API Service";
        }
        [HttpPost("add")]
        public ActionResult AddStockPrice(List<StockDetailsRequest> listStockDetailsRequest)
        {
            
            try
            {
                return Ok(_stockService.AddStockDetails(listStockDetailsRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("get/{companyCode}")]
        public ActionResult GetAllStockDetails(string companyCode)
        {            
            try
            {
                return Ok(_stockService.GetAllStockDetails(companyCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("delete/{id}")]
        public ActionResult DeleteStock(string id)
        {            
            try
            {
                return Ok(_stockService.DeleteStockDetails(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Fetch/{startDate}/{endDate}")]
        public ActionResult GetStockDetails(DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(_stockService.GetStockDetails(startDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("empty")]
        public ActionResult GetEmptystock()
        {
            try
            {
                return Ok(_stockService.GetEmptyStock());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
