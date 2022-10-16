using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockMarket.Stock.DbSettings.Interface;
using StockMarket.Stock.DTO.Request;
using StockMarket.Stock.Models;
using StockMarket.Stock.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool AddStockDetails(List<StockDetailsRequest> listStockDetailsRequest)
        {
            try
            {
                List<StockDetails> lstStockDetails = _stockDetails.Find(d => d.CompanyCode == (listStockDetailsRequest.FirstOrDefault().CompanyCode)).ToList();
                lstStockDetails = lstStockDetails == null ? new List<StockDetails>() : lstStockDetails;
                if (lstStockDetails.Count > 0)
                {
                    foreach (var stock in lstStockDetails)
                    {
                        var stock1 = listStockDetailsRequest.Where(c => c.Id == stock.Id).FirstOrDefault();
                        if (stock1 == null)
                        {
                            var deleteFilter = Builders<StockDetails>.Filter.Eq(e => e.Id, stock.Id);
                            _stockDetails.DeleteOne(deleteFilter);
                        }
                    }
                }
                foreach (var stockDetailsRequest in listStockDetailsRequest)
                {
                    StockDetails stockDetails = lstStockDetails.Where(d => d.CompanyCode == stockDetailsRequest.CompanyCode && d.Id == stockDetailsRequest.Id).FirstOrDefault();
                    if (stockDetails != null)
                    {
                        var filter = Builders<StockDetails>.Filter.Eq(e => e.Id, stockDetailsRequest.Id);
                        var update = Builders<StockDetails>.Update.Set(t => t.StockPrice, stockDetailsRequest.StockPrice);
                        _stockDetails.UpdateOne(filter, update);
                    }

                    else
                    {
                        stockDetails = new StockDetails();
                        stockDetails.CompanyCode = stockDetailsRequest.CompanyCode;
                        stockDetails.StockPrice = stockDetailsRequest.StockPrice;
                        stockDetails.StockMaxPrice = stockDetailsRequest.StockMaxPrice ?? 0;
                        stockDetails.StockMinPrice = stockDetailsRequest.StockMinPrice ?? 0;
                        stockDetails.StockAveragePrice = stockDetailsRequest.StockAveragePrice ?? 0;
                        stockDetails.StartDate = stockDetailsRequest.StartDate;
                        stockDetails.EndDate = stockDetailsRequest.EndDate;
                        stockDetails.IsDelete = 0;
                        stockDetails.InsertDate = DateTime.Now;
                        stockDetails.Time = stockDetails.StartDate.TimeOfDay;
                        _stockDetails.InsertOne(stockDetails);
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<StockDetails> GetAllStockPriceDetails(string companyCode)
        {
            try
            {
                var stockInfo = _stockDetails.Find(d => d.IsDelete == 0 && d.CompanyCode == companyCode).ToList();
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
                var deleteFilter = Builders<StockDetails>.Filter.Eq(e => e.Id, id);
                _stockDetails.DeleteOne(deleteFilter);
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
                List<StockDetails> stockInfo = new List<StockDetails>();
                if (startDate != null && endDate != null)
                {
                    stockInfo = _stockDetails.Find(d => (DateTime)startDate <= d.StartDate && endDate >= d.StartDate).ToList();

                }
                return stockInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StockDetailsRequest GetEmptyStock()
        {
            StockDetailsRequest stockDetailsRequest = new StockDetailsRequest();
            stockDetailsRequest.StartDate = DateTime.Now;
            //stockDetailsRequest.Id = Guid.NewGuid().ToString();
            stockDetailsRequest.Index = 0;
            return stockDetailsRequest;
        }
        public bool DeleteStockByCompanyCode(string companyCode)
        {
            try
            {
                var deleteFilter = Builders<StockDetails>.Filter.Eq(e => e.CompanyCode, companyCode);
                _stockDetails.DeleteOne(deleteFilter);
                Consume(companyCode);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Consume(string companyCode)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672")
                };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                Consume(channel);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void Consume(IModel channel)
        {
            try
            {
                channel.QueueDeclare("Company Delete",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };

                channel.BasicConsume("Company Delete", true, consumer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
