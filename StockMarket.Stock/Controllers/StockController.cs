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
        public void AddStockPrice(StockDetailsRequest stockDetailsRequest)
        {
            var result = _stockService.AddStockDetails(stockDetailsRequest);
        }

        [HttpGet("get")]
        public ActionResult<List<StockDetails>> GetAllStockDetails()
        {
            var result = _stockService.GetAllStockDetails();
            return result;
        }

        [HttpPost("delete")]
        public void DeleteStock(string id)
        {
            var result = _stockService.DeleteStockDetails(id);
        }

        [HttpGet("Fetch")]
        public ActionResult<List<StockDetails>> GetStockDetails(DateTime startDate,DateTime endDate)
        {
            var result = _stockService.GetStockDetails(startDate,endDate);
            return result;
        }
    }
}
