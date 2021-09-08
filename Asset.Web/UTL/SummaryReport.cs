using Asset.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.UTL
{
    public class SummaryReport
    {
        private readonly ApplicationDbContext _db;
        public SummaryReport(ApplicationDbContext db)
        {
            _db = db;
        }

        public decimal UsingNetAssetBasis(int CorpId)
        {
            int NumberOfMember = 0;
            decimal MyUsingAssetBasis = Tna(CorpId) / NumberOfMember;
            return MyUsingAssetBasis;
        }
        public decimal Tna(int CorpId)
        {
            var MyAssetLiabilitylist = _db.AssetLiabilityView.Where(k => k.CorpId == CorpId).ToList();
            decimal MyNonCuurentAsset = MyAssetLiabilitylist.Where(k => k.Type == "Non-Current" && k.BasicType == "Asset").Sum(k => k.Value);
            decimal MyCuurentAsset = MyAssetLiabilitylist.Where(k => k.Type == "Current" && k.BasicType == "Asset").Sum(k => k.Value);
            decimal MyCuurentLiability = MyAssetLiabilitylist.Where(k => k.Type == "Current" && k.BasicType == "Liability").Sum(k => k.Value);
            var TotalAsset = MyNonCuurentAsset + MyCuurentAsset;
            decimal MyTNA = TotalAsset - MyCuurentAsset;
            return MyTNA;
        }
        public decimal MV(decimal Epmc, decimal Epr)
        {
            decimal MyMv = Epmc * Epr;
            return MyMv;
        }
        public decimal Valution (int CorpId)
        {
            decimal  CorpEmpc = 1;
            decimal CorpEpr = 1;
            decimal TheValuation = UsingNetAssetBasis(CorpId) + MV(CorpEmpc, CorpEpr);
            return TheValuation;
        }
        public decimal CorpNonCurrentAsset(int CorpId)
        {
           //// var MyAssetLiabilityType = _db.
            var MyAssetLiabilitylist = _db.AssetLiabilityView.Where(k => k.CorpId == CorpId).ToList();
            decimal MyNonCuurentAsset = MyAssetLiabilitylist.Where(k => k.BasicType == "Asset" && k.Tenure >12).Sum(k=>k.Value);
            decimal MyCurentAsset = MyAssetLiabilitylist.Where(k => k.BasicType == "Asset" && k.Tenure <= 12).Sum(k => k.Value);
            decimal MyCurentLiability = MyAssetLiabilitylist.Where(k => k.BasicType == "Liability" && k.Tenure <= 12).Sum(k => k.Value);
            var TotalAsset = MyNonCuurentAsset + MyCurentAsset;
            decimal MyTNA = TotalAsset - MyCurentLiability;
            return MyTNA;
        }
          
       
    }
}
