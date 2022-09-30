using StockMarket.Stock.DTO.Request;
using StockMarket.Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.Repository.Interface
{
    public interface IStockRepo
    {
        bool AddStockDetails(StockDetailsRequest stockDetailsRequest);

        List<StockDetails> GetAllStockPriceDetails();

        bool DeleteStock(string id);

        List<StockDetails> GetStockDetails(DateTime startDate, DateTime endDate);

    }
}
