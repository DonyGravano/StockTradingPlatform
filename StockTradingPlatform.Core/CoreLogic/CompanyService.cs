﻿using System;
using System.Collections.Generic;
using StockTradingPlatform.Core.DataAccess;
using StockTradingPlatform.Core.Model;

namespace StockTradingPlatform.Core.CoreLogic
{
    public class CompanyService
    {
        private readonly CompanyContext _companyContext;
        private readonly OrderContext _orderContext;

        public CompanyService(CompanyContext companyContext, OrderContext orderContext)
        {
            _companyContext = companyContext;
            _orderContext = orderContext;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _companyContext.GetCompanies();
        }

        public Company GetCompany(string symbol)
        {
            return _companyContext.GetCompany(symbol);
        }

        // TODO: Decide whether this should return the company. Nothing get's set on the company so the object doesn't change but it's probably a good idea
        public Company AddCompany(Company company)
        {
            if (_companyContext.GetCompany(company.Symbol) != null) throw new Exception("A company with this symbol already exists");

            return _companyContext.AddCompany(company);
        }

        public Order IssueShares(string symbol, int price, int quantity)
        {
            if (_companyContext.GetCompany(symbol) == null) throw new Exception("No company with this symbol exists");

            return _orderContext.AddOrder(new Order(symbol, price, price, quantity, OrderType.Sell));
        }
    }
}