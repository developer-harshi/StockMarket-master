using MongoDB.Driver;
using StockMarket.Company.DbSettings.Interface;
using StockMarket.Company.DTO.Request;
using StockMarket.Company.DTO.Response;
using StockMarket.Company.Models;
using StockMarket.Company.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.Repository.Service
{

    public class CompanyRepo : ICompanyRepo
    {
        private readonly IMongoCollection<CompanyDetails> _companyDetails;
        private readonly IMongoCollection<StockDetails> _stockDetails;
        public CompanyRepo(IStockDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _companyDetails = database.GetCollection<CompanyDetails>(settings.StockCollectionName);
            _stockDetails = database.GetCollection<StockDetails>(settings.StockDetailsCollectionName);
        }


        public bool RegisterCompany(CompanyDetailsRequest company)
        {
            try
            {
                CompanyDetails companyDetails = new CompanyDetails();
                companyDetails.CompanyCEO = company.CompanyCEO;
                companyDetails.CompanyCode = company.CompanyCode;
                companyDetails.CompanyName = company.CompanyName;
                companyDetails.CompanyTurnOver = company.CompanyTurnOver;
                companyDetails.CompanyWebsite = company.CompanyWebsite;
                companyDetails.stockExchangeEnum = Convert.ToInt32(company.stockExchangeEnum);
                companyDetails.IsDelete = 0;
                companyDetails.InsertDate = DateTime.Now;

                _companyDetails.InsertOne(companyDetails);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CompanyDetails> GetCompanyDetails(string companyCode)
        {
            try
            {
                //var companyDetails = new CompanyDetailsResponse();
                //CompanyDetailsResponseList companyDetailsList = new CompanyDetailsResponseList();
                var company = _companyDetails.Find(u => u.CompanyCode == companyCode).ToList();
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CompanyDetails> GetAllCompanyDetails()
        {
            try
            {
                //var companyDetails = new CompanyDetailsResponse();
                //CompanyDetailsResponseList companyDetailsList = new CompanyDetailsResponseList();        
                var company = _companyDetails.Find(d => d.IsDelete == 0).ToList();
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool DeleteCompany(string companyCode)
        {
            try
            {
                var filter = Builders<CompanyDetails>.Filter.Eq(e => e.CompanyCode, companyCode);
                var update = Builders<CompanyDetails>.Update.Set(t => t.IsDelete, 1);
                _companyDetails.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
    }
}
