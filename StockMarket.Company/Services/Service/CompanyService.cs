using StockMarket.Company.DTO.Request;
using StockMarket.Company.DTO.Response;
using StockMarket.Company.Models;
using StockMarket.Company.Repository.Interface;
using StockMarket.Company.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Company.Services.Service
{
    public class CompanyService: ICompanyService
    {
        readonly ICompanyRepo _companyRepo;

        public CompanyService(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }
        public bool RegisterCompany(CompanyDetailsRequest companyDetails)
        {
            return _companyRepo.RegisterCompany(companyDetails);
        }

        public List<CompanyDetails> GetCompanyDetails(string companyCode)
        {
            return _companyRepo.GetCompanyDetails(companyCode);
        }
        public List<CompanyDetails> GetAllCompanyDetails()
        {
            return _companyRepo.GetAllCompanyDetails();
        }

        public bool DeleteCompanyDetails(string companyCode)
        {
            return _companyRepo.DeleteCompany(companyCode);
        }

        public bool AddStockDetails(StockDetailsRequest stockDetailsRequest)
        {
            return _companyRepo.AddStockDetails(stockDetailsRequest);
        }

        public List<StockDetails> GetAllStockDetails()
        {
            return _companyRepo.GetAllStockPriceDetails();
        }

        public bool DeleteStockDetails(string id)
        {
            return _companyRepo.DeleteStock(id);
        }

        public bool AddUser(User user)
        {
            return _companyRepo.AddUser(user);
        }

        public User GetUser()
        {
            return _companyRepo.GetUser();
        }

        public CompanyDetails GetCompany(string companyCode)
        {
            return _companyRepo.GetCompany(companyCode);
        }
    }
}
