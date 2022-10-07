using StockMarket.Stock.DTO.Request;
using StockMarket.Stock.Models;
using StockMarket.Stock.Repository.Interface;
using StockMarket.Stock.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.Services.Servicee
{
    public class StockService: IStockService
    {
        readonly IStockRepo _stockRepo;

        public StockService(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }
        public bool AddStockDetails(List<StockDetailsRequest> listStockDetailsRequest)
        {
            return _stockRepo.AddStockDetails(listStockDetailsRequest);
        }

        public List<StockDetails> GetAllStockDetails()
        {
            return _stockRepo.GetAllStockPriceDetails();
        }

        public bool DeleteStockDetails(string id)
        {
            return _stockRepo.DeleteStock(id);
        }

        public List<StockDetails> GetStockDetails(DateTime startDate, DateTime endDate)
        {
            return _stockRepo.GetStockDetails(startDate, endDate);
        }

        public StockDetailsRequest GetEmptyStock()
        {
            return _stockRepo.GetEmptyStock();
        }
    }
}
