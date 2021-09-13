using Asset.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asset.Web.Models;

namespace Asset.Web.ViewModels
{
    public class AME
    {
        private readonly ApplicationDbContext _db;
        public AME(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<AverageMaintanableEarnings> AveEarningList()
        {
            var EarningArray = _db.AverageMaintanableEarnings.ToList();
            return EarningArray;
        }
        public List<AverageMaintanableEarnings> AveEarningList(int Corpid)
        {
            var EarningArray = _db.AverageMaintanableEarnings.Where(k=>k.CorpId == Corpid).ToList();
            return EarningArray;
        }

        public List<AverageMaintanableEarnings> ThreeYear(int CorpId, string Year)
        {
            var ListRevenueThreeYear = _db.AverageMaintanableEarnings.Where(k => k.CorpId == CorpId && k.ValueDate.Year <= System.DateTime.Now.Year - 2).ToList();
            return ListRevenueThreeYear;
        }
        public decimal RevenueForThreeYear(int CorpId, string Year)
        {
            var ListRevenueThreeYear = ThreeYear(CorpId, Year).Sum(k=>k.Revenue);
            return ListRevenueThreeYear;
        }
        public decimal ThreeYearCostOfGoodSold(int CorpId, string Year)
        {
            var ListYearCostOfGoodSoldThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Operatingexpenses);
            return ListYearCostOfGoodSoldThreeYear;
        }
        public decimal OperatingExpenses(int CorpId, string Year)
        {
            var ListOperatingExpensesThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Operatingexpenses);
            return ListOperatingExpensesThreeYear;
        }
        public decimal OtherExpenses(int CorpId, string Year)
        {
            var ListOtherExpensesThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Otherexpenses);
            return ListOtherExpensesThreeYear;
        }
        public decimal Depreciation(int CorpId, string Year)
        {
            var ListDepreciationThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Depreciation);
            return ListDepreciationThreeYear;
        }
        public decimal ProfitBeforeTax(int CorpId, string Year)
        {
            var ListProfitBeforeTaxThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Profitbeforetax);
            return ListProfitBeforeTaxThreeYear;
        }
        public decimal IncomeTaxPaid(int CorpId, string Year)
        {
            var ListIncomeTaxPaidThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Incometaxpaid);
            return ListIncomeTaxPaidThreeYear;
        }
        public decimal ProfitAfterTax(int CorpId, string Year)
        {
            var ListProfitAfterTaxThreeYear = ThreeYear(CorpId, Year).Sum(k => k.Profitaftertax);
            return ListProfitAfterTaxThreeYear;
        }
      


    }
}
