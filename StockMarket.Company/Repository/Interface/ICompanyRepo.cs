using StockMarket.Company.DTO.Request;
using StockMarket.Company.DTO.Response;
using StockMarket.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.Repository.Interface
{
    public interface ICompanyRepo
    {

        bool RegisterCompany(CompanyDetailsRequest company);
        List<CompanyDetails> GetCompanyDetails(string companyCode);
        List<CompanyDetails> GetAllCompanyDetails();

        bool DeleteCompany(string companyCode);

        bool AddStockDetails(StockDetailsRequest stockDetailsRequest);

        List<StockDetails> GetAllStockPriceDetails();

        bool DeleteStock(string id);
    }
}
