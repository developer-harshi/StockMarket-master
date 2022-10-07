using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StockMarket.Company.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.Models
{
    public class CompanyDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string CompanyCode { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCEO { get; set; }
        [Required]
        public string CompanyTurnOver { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }
         
        public  int StockExchangeEnum {get;set;}

        public int IsDelete { get; set; }
        public DateTime? InsertDate { get; set; }



    }
}
