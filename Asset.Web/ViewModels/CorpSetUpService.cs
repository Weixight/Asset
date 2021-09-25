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

            var result =  _db.SaveChanges();
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
            var result =  _db.SaveChanges();
            return result;

           
        }

        public List<CorpEarning> MyCorpEarning(int CorpId)
        {
            var MyCorpEarning = _db.MyEarning.Where(K => K.Corpid == CorpId && K.ValueDate.Year >= System.DateTime.Now.AddYears(-2).Year).ToList();
            List<CorpEarning> MyCurrentEarning = new List<CorpEarning>();
            MyCorpEarning = _db.MyEarning.Where(K => K.Corpid == CorpId ).ToList();
            MyCorpEarning = ConvertThreeYears(MyCorpEarning);
            return MyCurrentEarning;
        }

        public List<CorpEarning>ConvertThreeYears(List<CorpEarning> corpEarnings)
        {
            List<CorpEarning> ThreeYearsEarning = new List<CorpEarning>();
            var MySetupList = _db.OurCorpEarningSetup.Where(l => l.CorpReg.id == corpEarnings.FirstOrDefault().Corpid);
            decimal YearOneValue = 0;
            decimal YearTwoValue = 0;
            decimal YearThreeValue = 0;
            DateTime valueDate1 = new DateTime();
            DateTime valueDate2 = new DateTime();
            DateTime valueDate3 = new DateTime();
            foreach (var item in corpEarnings)
            {
                var GetMySetUp = MySetupList.FirstOrDefault(K => K.id == item.CorpEarningid);
                var CorpReg = _db.corpRegs.FirstOrDefault(K => K.id == corpEarnings.FirstOrDefault().id);
                if(item.ValueDate.Year == System.DateTime.Now.Year)
                {
                    YearOneValue = item.ValueAmount;
                    valueDate1 = item.ValueDate;
                }
                else if (item.ValueDate.Year == System.DateTime.Now.AddYears(-1).Year)
                {
                    YearTwoValue = item.ValueAmount;
                    valueDate2 = item.ValueDate;
                }
                else if (item.ValueDate.Year == System.DateTime.Now.AddYears(-2).Year)
                {
                    YearThreeValue = item.ValueAmount;
                    valueDate3 = item.ValueDate;
                }
                ThreeYearsEarning.Add(new CorpEarning
                {
                      id = item.id,
                     CalculatedItem = GetMySetUp.CalculatedItemName,
                     CorpEarningid = item.CorpEarningid,
                     Corpid = item.Corpid,
                     KorpEarning = item.KorpEarning,
                     ValueAmount = item.ValueAmount,
                     ValueDate = item.ValueDate,
                     CorpReg = CorpReg,
                     Year_One = YearOneValue,
                     Year_Two = YearTwoValue,
                     Year_Three = YearThreeValue,


                    
                }) ;
               

            }
            return ThreeYearsEarning;
        }
       
    }
}
