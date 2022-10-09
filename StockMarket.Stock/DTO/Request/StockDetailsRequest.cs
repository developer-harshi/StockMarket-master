using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Stock.DTO.Request
{
    public class StockDetailsRequest
    {
        public string CompanyCode { get; set; }

        [Required]
        public decimal StockPrice { get; set; }
        //[Required]
        public decimal? StockMaxPrice { get; set; }
        //[Required]
        public decimal? StockMinPrice { get; set; }
        //[Required]
        public decimal? StockAveragePrice { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        //[Required]
        public DateTime? EndDate { get; set; }
        public string Id { get; set; }
        public int Index { get; set; }
    }
}
