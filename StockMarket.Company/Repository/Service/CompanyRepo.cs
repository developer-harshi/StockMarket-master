using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using StockMarket.Company.DbSettings.Interface;
using StockMarket.Company.DTO.Request;
using StockMarket.Company.Models;
using StockMarket.Company.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StockMarket.Company.Repository.Service
{

    public class CompanyRepo : ICompanyRepo
    {
        private readonly IMongoCollection<CompanyDetails> _companyDetails;
        public readonly IMongoCollection<User> _user;
        public CompanyRepo(IStockDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _companyDetails = database.GetCollection<CompanyDetails>(settings.StockCollectionName);
            _user = database.GetCollection<User>(settings.UserCollectionName);
        }


        public bool RegisterCompany(CompanyDetailsRequest company)
        {
            try
            {
                var companycode = _companyDetails.Find(u => u.CompanyCode == company.CompanyCode).FirstOrDefault();
                if (companycode == null && company.IsAdd == true)
                {
                    CompanyDetails companyDetails = new CompanyDetails();
                    companyDetails.CompanyCEO = company.CompanyCEO;
                    companyDetails.CompanyCode = company.CompanyCode;
                    companyDetails.CompanyName = company.CompanyName;
                    companyDetails.CompanyTurnOver = company.CompanyTurnOver;
                    companyDetails.CompanyWebsite = company.CompanyWebsite;
                    //companyDetails.StockExchangeEnum = Convert.ToInt32(company.StockExchangeEnum);
                    companyDetails.StockExchange = company.StockExchangeEnum;
                    companyDetails.IsDelete = 0;
                    companyDetails.InsertDate = DateTime.Now;
                    companyDetails.UserName = company.UserName.ToLower();
                    _companyDetails.InsertOne(companyDetails);
                    return true;
                }
                else if (companycode != null && company.IsAdd == false)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Company code already exist .");
                }
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
                List<CompanyDetails> company = _companyDetails.Find(u => u.IsDelete == 0 && u.CompanyCode.ToLower().Contains((companyCode.ToLower()))).ToList();


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
                RabbitMQPosting(companyCode);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #region Commented by me 
        //public bool AddStockDetails(StockDetailsRequest stockDetailsRequest)
        //{
        //    try
        //    {
        //        StockDetails stockDetails = new StockDetails();
        //        stockDetails.CompanyCode = stockDetailsRequest.CompanyCode;
        //        stockDetails.StockPrice = stockDetailsRequest.StockPrice;
        //        stockDetails.StockMaxPrice = stockDetailsRequest.StockMaxPrice;
        //        stockDetails.StockMinPrice = stockDetailsRequest.StockMinPrice;
        //        stockDetails.StockAveragePrice = stockDetailsRequest.StockAveragePrice;
        //        stockDetails.StartDate = stockDetailsRequest.StartDate;
        //        stockDetails.EndDate = stockDetailsRequest.EndDate;
        //        stockDetails.IsDelete = 0;
        //        stockDetails.InsertDate = DateTime.Now;
        //        _stockDetails.InsertOne(stockDetails);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public List<StockDetails> GetAllStockPriceDetails()
        //{
        //    try
        //    {
        //        var stockInfo = _stockDetails.Find(d => d.IsDelete == 0).ToList();
        //        return stockInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool DeleteStock(string id)
        //{
        //    try
        //    {
        //        var filter = Builders<StockDetails>.Filter.Eq(e => e.Id, id);
        //        var update = Builders<StockDetails>.Update.Set(t => t.IsDelete, 1);
        //        _stockDetails.UpdateOne(filter, update);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion Commented by me 
        public bool AddUser(User user)
        {
            try
            {
                _user.InsertOne(user);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public User GetUser()
        {
            try
            {
                User user = new User();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool FindUser(string email, string password)
        {
            try
            {
                var user = Builders<User>.Filter;
                var filter1 = user.Eq(e => e.Email, email);
                var filter2 = user.Eq(e => e.Password, password);
                var finalFilter = user.And(filter1, filter2);
                var filter = _user.Find(finalFilter);
                if (filter != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CompanyDetailsRequest GetCompany(string companyCode)
        {
            try
            {
                CompanyDetailsRequest companyDetailsRequest = new CompanyDetailsRequest();
                CompanyDetails company = _companyDetails.Find(u => u.CompanyCode == companyCode).FirstOrDefault();
                if (company == null)
                {
                    companyDetailsRequest.IsAdd = true;
                }
                else
                {
                    companyDetailsRequest.CompanyCEO = company.CompanyCEO;
                    companyDetailsRequest.CompanyCode = company.CompanyCode;
                    companyDetailsRequest.CompanyName = company.CompanyName;
                    companyDetailsRequest.CompanyTurnOver = company.CompanyTurnOver;
                    companyDetailsRequest.CompanyWebsite = company.CompanyWebsite;
                    companyDetailsRequest.IsAdd = false;
                    companyDetailsRequest.StockExchangeEnum = company.StockExchange;

                }
                return companyDetailsRequest;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void Publish(IModel channel, string companyCOde)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);
            //var count = 0;

            var message = new { Name = "Producer", Message = $"CompanyCode: {companyCOde}" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { { "account", "update" } };

            channel.BasicPublish("demo-fanout-exchange", "account.new", properties, body);


        }

        public bool RabbitMQPosting(string companyCode)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672")
                };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                channel.QueueDeclare("Company Delete", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var message = new { Name = "Producer", Message = $"CompanyCode: {companyCode}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "Company Code", null, body);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
