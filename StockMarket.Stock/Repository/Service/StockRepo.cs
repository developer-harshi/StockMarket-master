using MongoDB.Driver;
using StockMarket.Stock.DbSettings.Interface;
using StockMarket.Stock.DTO.Request;
using StockMarket.Stock.Models;
using StockMarket.Stock.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.Repository.Service
{
    public class StockRepo : IStockRepo
    {
        private readonly IMongoCollection<StockDetails> _stockDetails;

        public StockRepo(IStockDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _stockDetails = database.GetCollection<StockDetails>(settings.StockDetailsCollectionName);
        }

        public bool AddStockDetails(StockDetailsRequest stockDetailsRequest)
        {
            try
            {
                StockDetails stockDetails = new StockDetails();
                stockDetails.CompanyCode = stockDetailsRequest.CompanyCode;
                stockDetails.StockPrice = stockDetailsRequest.StockPrice;
                stockDetails.StockMaxPrice = stockDetailsRequest.StockMaxPrice;
                stockDetails.StockMinPrice = stockDetailsRequest.StockMinPrice;
                stockDetails.StockAveragePrice = stockDetailsRequest.StockAveragePrice;
                stockDetails.StartDate = stockDetailsRequest.StartDate;
                stockDetails.EndDate = stockDetailsRequest.EndDate;
                stockDetails.IsDelete = 0;
                stockDetails.InsertDate = DateTime.Now;

                _stockDetails.InsertOne(stockDetails);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<StockDetails> GetAllStockPriceDetails()
        {
            try
            {
                var stockInfo = _stockDetails.Find(d => d.IsDelete == 0).ToList();
                return stockInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteStock(string id)
        {
            try
            {
                var filter = Builders<StockDetails>.Filter.Eq(e => e.Id, id);
                var update = Builders<StockDetails>.Update.Set(t => t.IsDelete, 1);
                _stockDetails.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<StockDetails> GetStockDetails(DateTime startDate, DateTime endDate)
        {
            try
            {
                var stockInfo = _stockDetails.Find(d => d.StartDate >= (DateTime?)startDate && d.EndDate <= (DateTime?)endDate).ToList();
                return stockInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
