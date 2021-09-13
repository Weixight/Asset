using Asset.Web.Data;
using Asset.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.ViewModels
{
    public class CorpSetUpService
    {
        private readonly ApplicationDbContext _db;
        public CorpSetUpService (ApplicationDbContext db)
        {
            _db = db;

        }
        public List<OurCorpSetUp> MyCorpSetUp(string CorpName)
        {
            List<OurCorpSetUp> ourCorpSetUpLsit = new List<OurCorpSetUp>();
            var MySetupList = _db.OurCorpEarningSetup.Where(l=>l.CorpReg.Name ==CorpName);
            var EarningType = _db.AverageMaintanableEarningsWeighteds.ToList();
            var GetMyCorpDetails = _db.corpRegs.FirstOrDefault(L =>L.Name ==CorpName);
           
                foreach(var item in EarningType)
                {
                var GetMySetUp = MySetupList.FirstOrDefault(K => K.CorpEarning.id == item.id);
                if (MySetupList.ToList().Count !=0)
                    ourCorpSetUpLsit.Add(new OurCorpSetUp
                    {
                        id = item.id,
                        CalculatedItemName = GetMySetUp.CorpEarning.CalculatedItem,
                        CorpEarningid = GetMySetUp.CorpEarning.id,
                        Applied = GetMySetUp.Applied,
                        CorpName = GetMyCorpDetails.Name,
                        CorpReg = GetMySetUp.CorpReg,
                        CorpEarning = GetMySetUp.CorpEarning,


                    });
                else
                {
                    ourCorpSetUpLsit.Add(new OurCorpSetUp
                    {
                        id = item.id,
                        CalculatedItemName = item.CalculatedItem,
                    });
                }
             
                }
           
            return ourCorpSetUpLsit;
        }

        public async Task<int> CreateCorpEarningAsync(List<OurCorpSetUp> ourCorpSetUps ,string Name)
        {
            var EarningType = _db.AverageMaintanableEarningsWeighteds.ToList();
            var CorpDetails = _db.corpRegs.FirstOrDefault(K => K.Name == Name);
            foreach (var item in ourCorpSetUps)
            {
                var MyEarningType = EarningType.FirstOrDefault(K => K.id == item.id);
                _db.OurCorpEarningSetup.Add(new OurCorpSetUp
                {
                    
                    Applied = item.Applied,
                    CalculatedItemName = item.CalculatedItemName,
                    CorpName = Name,
                    CorpReg = CorpDetails,
                    CorpEarning = MyEarningType,
                    CorpEarningid = MyEarningType.id,
                    Corpid = CorpDetails.id,
                    DateCreated = System.DateTime.Now, 
                   
                }) ;
            }

            var result = await _db.SaveChangesAsync();
            return result;
        }
        public async Task<int> UpdateEarning(List<OurCorpSetUp> ourCorpSetUps, int Corpid)
        {
            List<OurCorpSetUp> MygradeLeave = new List<OurCorpSetUp>();
            var existing = _db.OurCorpEarningSetup.Where(x => x.Corpid == Corpid).ToList();

            _db.RemoveRange(existing);
            foreach (var item in ourCorpSetUps)
            {
                var EarnUpdate = existing.FirstOrDefault(l => l.CorpEarningid == item.CorpEarningid);

                EarnUpdate.Applied = item.Applied;

                _db.OurCorpEarningSetup.Update(EarnUpdate);


            }
            var result = await _db.SaveChangesAsync();
            return result;

           
        }
    }
}
