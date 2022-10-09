using StockMarket.Company.DTO.Request;
using StockMarket.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.Services.Interface
{
    public interface ICompanyService
    {

        bool RegisterCompany(CompanyDetailsRequest companyDetails);

        List<CompanyDetails> GetAllCompanyDetails();
        List<CompanyDetails> GetCompanyDetails(string companyCode);
        bool DeleteCompanyDetails(string companyCode);
        #region Commented By me 
        //bool AddStockDetails(StockDetailsRequest stockDetailsRequest);

        //List<StockDetails> GetAllStockDetails();

        //bool DeleteStockDetails(string id);
        #endregion Commented By me 
        bool AddUser(User user);
        User GetUser();
        CompanyDetailsRequest GetCompany(string companyCode);

    }
}
