using Asset.Web.Data;
using Asset.Web.UTL;
using Asset.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class AssetLiabilityRun
    {
        private readonly ApplicationDbContext _db;

        public AssetLiabilityRun(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<AssetLiabilityView> PerCopAssetLiability(string CorpName)
        {
            List<AssetLiabilityView> MyLiabilityAsset = new List<AssetLiabilityView>();
            var Asset = _db.KopAssets.Where(k => k.CorpNName == CorpName).ToList();
            var Liabilities = _db.Liabilities.Where(k => k.CorpNName == CorpName).ToList();
            foreach(var item in Asset)
            {
                string MyTenureType = "";
                if(item.Tenure>12)
                {
                    MyTenureType = TenureType.NonCurrentAsset;
                }
                else
                {
                    MyTenureType = TenureType.CurrentAsset;

                }
                MyLiabilityAsset.Add(new AssetLiabilityView
                {
                    id = item.id,
                    CorpId = item.CorpId,
                    AssetDate = item.AssetDate,
                    CopAssetRegNo = item.CopAssetRegNo,
                    CorpNName = item.CorpNName,
                    Name = item.Name,
                    Tenure = item.Tenure,
                    Type = item.Type,
                    Value = item.Value,
                    TenureName = MyTenureType,
                    ItemType = TenureType.Asset
                });
            }
            foreach(var items in Liabilities)

            {
                string MyTenureType = "";
                if (items.Tenure > 12)
                {
                    MyTenureType = TenureType.NonCurrentLiability;
                }
                else
                {
                    MyTenureType = TenureType.CurrentLiability;

                }
                MyLiabilityAsset.Add(new AssetLiabilityView
                {
                    id = items.id,
                    CorpId = items.CorpId,
                    AssetDate = items.AssetDate,
                    CopAssetRegNo = items.CopAssetRegNo,
                    CorpNName = items.CorpNName,
                    Name = items.Name,
                    Tenure = items.Tenure,
                    Type = items.Type,
                    Value = items.Value,
                    TenureName = MyTenureType,
                    ItemType = TenureType.Liability
                });
            }
            return MyLiabilityAsset;
        }
        public List<KopAsset> CorpAsset (string CorpName)
        {
            var MyCorpAsset = _db.KopAssets.Where(k => k.CorpNName == CorpName).ToList();
            return MyCorpAsset;
        }
        public List<Liability> CorPLiability(string CorpName)
        {
            var MyCorpLiability = _db.Liabilities.Where(k => k.CorpNName == CorpName).ToList();
            return MyCorpLiability;
        }

        public List<KopAsset> AllCorpAsset ()
        {
            List<CorpReg> CorpList = new List<CorpReg>();
            List<KopAsset> kopAsseList = new List<KopAsset>();
            CorpList = _db.corpRegs.ToList();
            foreach(var item in CorpList)
            {
                var CorpAsset = _db.KopAssets.Where(k => k.CorpNName == item.Name).ToList();
                kopAsseList.AddRange(CorpAsset);
            }
            return kopAsseList;
        }
        public List<KopAsset> AllCorpAsset(string Name)
        {
            List<KopAsset> kopAsseList = new List<KopAsset>();

            kopAsseList = _db.KopAssets.Where(k => k.CorpNName == Name).ToList();
            
            return kopAsseList;
        }
        public List<Liability> AllKorpLiability()
        {
            List<CorpReg> CorpList = new List<CorpReg>();
            List<Liability> kopLiabilityList = new List<Liability>();
            CorpList = _db.corpRegs.ToList();
            foreach (var item in CorpList)
            {
                var CorpAsset = _db.Liabilities.Where(k => k.CorpNName == item.Name).ToList();
                kopLiabilityList.AddRange(CorpAsset);
            }
            return kopLiabilityList;
        }
        public List<Liability> AllKorpLiability(string Name)
        {
            List<Liability> kopLiabilityList = new List<Liability>();


            kopLiabilityList = _db.Liabilities.Where(k => k.CorpNName == Name).ToList();
                
            
            return kopLiabilityList;
        }
        public decimal AllTotalAsset()
        {
            var Asset = AllCorpAsset().Sum(k=>k.Value);

            return Asset;
        }
        public decimal AllTotalAsset(string Name)
        {
            var Asset = AllCorpAsset(Name).Sum(k => k.Value);

            return Asset;
        }
        
        public decimal AllTotalLiability()
        {
            var Asset = AllKorpLiability().Sum(k => k.Value);

            return Asset;
        }
        public decimal AllTotalLiability(string Name)
        {
            var Asset = AllKorpLiability(Name).Sum(k => k.Value);

            return Asset;
        }
        public decimal AllNetAsset()
        {
            var Networt = AllTotalAsset() - AllTotalLiability();
            return Networt;
        }

        public decimal AllNetAsset(string Name)
        {
            var Networt = AllTotalAsset(Name) - AllTotalLiability(Name);
            return Networt;
        }
    }
}
