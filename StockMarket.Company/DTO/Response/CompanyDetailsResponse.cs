using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.DTO.Response
{
    public class CompanyDetailsResponse
    {
        public string CompanyCode { get; set; }
        
        public string CompanyName { get; set; }
        
        public string CompanyCEO { get; set; }
        
        public string CompanyTurnOver { get; set; }
     
        public string CompanyWebsite { get; set; }
       
        public string stockExchange { get; set; }

        public int IsDelete { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}
