using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.DbSettings.Interface
{
    public interface IStockDatabaseSettings
    {
        public string StockCollectionName { get; set; }
        public string StockDetailsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
