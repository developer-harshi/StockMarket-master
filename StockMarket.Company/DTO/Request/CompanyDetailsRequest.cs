﻿using System;
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
        public string CompanyTurnOver { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }

        public string stockExchangeEnum { get; set; } 
    }
}