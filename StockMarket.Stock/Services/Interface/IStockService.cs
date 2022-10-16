using StockMarket.Stock.DTO.Request;
using StockMarket.Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.Services.Interface
{
    public interface IStockService
    {
        bool AddStockDetails(List<StockDetailsRequest> listStockDetailsRequest);

        List<StockDetails> GetAllStockDetails(string companyCode);

        bool DeleteStockDetails(string id);

        List<StockDetails> GetStockDetails(DateTime startDate, DateTime endDate);
        StockDetailsRequest GetEmptyStock();
        bool DeleteStockByCompanyCode(string companyCode);
    }
}
