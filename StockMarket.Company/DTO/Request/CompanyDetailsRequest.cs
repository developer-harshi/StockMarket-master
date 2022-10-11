using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.DTO.Request
{
    public class CompanyDetailsRequest
    {  
        public string CompanyCode { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCEO { get; set; }
        [Required]
        public int CompanyTurnOver { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }

        public string StockExchangeEnum { get; set; }


        public bool IsAdd { get; set; }
        public string UserName { get; set; }
    }
}
