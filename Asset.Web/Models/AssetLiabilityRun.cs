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
    }
}
