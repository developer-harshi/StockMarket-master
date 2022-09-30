using StockMarket.Stock.DbSettings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.DbSettings
{
    public class StockDatabaseSettings: IStockDatabaseSettings
    {
        public string StockCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string StockDetailsCollectionName { get; set; }
    }
}
