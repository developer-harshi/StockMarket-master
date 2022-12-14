using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.Models
{
    public class StockDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CompanyCode { get; set; }

        [Required]
        public decimal StockPrice { get; set; }
        //[Required]
        public decimal StockMaxPrice { get; set; }
        //[Required]
        public decimal StockMinPrice { get; set; }
        //[Required]
        public decimal StockAveragePrice { get; set; }

        public int? IsDelete { get; set; }
        public DateTime? InsertDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        //[Required]
        public DateTime? EndDate { get; set; }
        public TimeSpan Time { get; set; }
    }
}
