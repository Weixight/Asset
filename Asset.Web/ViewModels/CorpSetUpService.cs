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
                        Corpid = GetMySetUp.Corpid,


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
            MyCorpEarning = _db.MyEarning.Where(K => K.Corpid == CorpId && K.ValueDate.AddYears(-2).Year <= System.DateTime.Now.Year ).ToList();
            MyCurrentEarning = ConvertThreeYears(MyCorpEarning);
            return MyCurrentEarning;
        }

        public List<CorpEarning>ConvertThreeYears(List<CorpEarning> corpEarnings)
        {
            List<CorpEarning> ThreeYearsEarning = new List<CorpEarning>();
            var MySetupList = _db.OurCorpEarningSetup.Where(l => l.CorpReg.id == corpEarnings.FirstOrDefault().Corpid);
            var MyPrimarySetup = _db.AverageMaintanableEarningsWeighteds.ToList();
            decimal YearOneValue = 0;
            decimal YearTwoValue = 0;
            decimal YearThreeValue = 0;
            DateTime valueDate1 = new DateTime();
            DateTime valueDate2 = new DateTime();
            DateTime valueDate3 = new DateTime();
            List<CorpEarning> TheCorpEarningList = new List<CorpEarning>();
            foreach (var item in MySetupList)
            {
                //var GetMySetUp = MySetupList.FirstOrDefault(K => K.id == item.CorpEarningid);
               // var GetMyPrimarySetUpItem = MySetupList.FirstOrDefault(K => K.id == item.CorpEarningid);
                var CorpReg = _db.corpRegs.FirstOrDefault(K => K.id == corpEarnings.FirstOrDefault().Corpid);
              
                var EarningMe = corpEarnings.Where(K => K.CorpEarningid == item.id).ToList();
                if (EarningMe.Count != 0)
                {
                    foreach (var itemYear in EarningMe)
                    {

                        if (itemYear.ValueDate.Year == System.DateTime.Now.Year)
                        {
                            itemYear.Year_One = itemYear.ValueAmount;
                            itemYear.Date_One = itemYear.Date_One;

                        }
                        else if (itemYear.ValueDate.Year == System.DateTime.Now.AddYears(-1).Year)
                        {
                            itemYear.Year_Two = itemYear.ValueAmount;
                            itemYear.Date_Two = itemYear.Date_Two;
                        }
                        else if (itemYear.ValueDate.Year == System.DateTime.Now.AddYears(-2).Year)
                        {
                            itemYear.Year_Three = itemYear.ValueAmount;
                            itemYear.Date_Three = itemYear.Date_Three;
                        }
                        TheCorpEarningList.Add(new CorpEarning
                        {
                            Year_One = itemYear.Year_One,
                            Year_Two = itemYear.Year_Two,
                            Year_Three = itemYear.Year_Three,
                            Date_One = itemYear.Date_One,
                            Date_Two = itemYear.Date_Two,
                            Date_Three = itemYear.Date_Three
                        });


                    }
                }
      

            }
            return TheCorpEarningList;
        }
        public List<CorpEarning>GetMyApplicableEarning(int CorpId)
        {
            List<CorpEarning> MyEarning = new List<CorpEarning>();
            var OurWeight = _db.EarninWeights.ToList();
            var PrimaryItemList = _db.AverageMaintanableEarningsWeighteds.ToList();
            var CorpReg = _db.corpRegs.FirstOrDefault(K=>K.id ==CorpId);
            var TheValueEarningList = _db.MyEarning.Where(K => K.Corpid == CorpId).ToList();
            var MyApplicable = _db.OurCorpEarningSetup.Where(K => K.Applied == true && K.Corpid == CorpId).ToList();
            foreach(var item in MyApplicable)
            {
                var TheitemValueList = TheValueEarningList.Where(K => K.CorpEarningid == item.id).ToList();
                if (TheitemValueList.Count() != 0)
                {
                    List<CorpEarning> earning = new List<CorpEarning>();
                    decimal Year1 = 0;
                    decimal Year2 = 0;
                    decimal Year3 = 0;
                    decimal TotalEarning = 0;
                    decimal TotalExpense = 0;
                    decimal AverageEarning = 0;
                    decimal Weight1 = 0;
                    decimal Weight2 = 0;
                    decimal Weight3 = 0;
                    DateTime date1 = new DateTime();
                    DateTime date2 = new DateTime();
                    DateTime date3 = new DateTime();


                    foreach (var itemValue in TheitemValueList)
                    {
                        if (System.DateTime.Now.Year == itemValue.ValueDate.Year)
                        {

                            Year1 = itemValue.ValueAmount;
                            date1 = System.DateTime.Now;
                            Weight1 = OurWeight.FirstOrDefault(K => K.ValueDate.Year == date1.Year).ValuePercentage;


                        }

                        else if (System.DateTime.Now.AddYears(-1).Year == itemValue.ValueDate.Year)
                        {
                            Year2 = itemValue.ValueAmount;
                            date2 = System.DateTime.Now.AddYears(-1);
                            Weight2 = OurWeight.FirstOrDefault(K => K.ValueDate.Year == date2.Year).ValuePercentage;

                        }
                        else if (System.DateTime.Now.AddYears(-2).Year == itemValue.ValueDate.Year)
                        {
                            Year3 = itemValue.ValueAmount;
                            date3 = System.DateTime.Now.AddYears(-2);
                            Weight3 = OurWeight.FirstOrDefault(K => K.ValueDate.Year == date3.Year).ValuePercentage;


                        }



                    }

                    var IsExpense = PrimaryItemList.FirstOrDefault(K => K.id == item.CorpEarningid).Purpose;


                    var MyIndividualEarning = TheValueEarningList.FirstOrDefault(K => K.CorpEarningid == item.id);
                    var MyPrimaryItem = PrimaryItemList.FirstOrDefault(K => K.id == item.CorpEarningid);
                    var Total = Year1 + Year2 + Year3;
                    var Average = Total / TheitemValueList.Count;


                    MyEarning.Add(new CorpEarning
                    {
                        id = MyIndividualEarning.id,
                        CalculatedItem = MyPrimaryItem.CalculatedItem,
                        KorpEarning = item,
                        CorpEarningid = item.id,
                        Corpid = CorpId,
                        CorpReg = CorpReg,
                        Year_One = Year1,
                        Year_Two = Year2,
                        Year_Three = Year3,
                        CreatedBy = MyIndividualEarning.CreatedBy,
                        DateCreated = MyIndividualEarning.DateCreated,
                        DateUpdated = MyIndividualEarning.DateUpdated,
                        Date_One = date1,
                        Date_Two = date2,
                        Date_Three = date3,
                        AverageValue = Average,
                        TotalEearning = TotalEarning,
                        Purpose = IsExpense,
                        AverageEarning = Average,
                        Weight1 = Weight1,
                        Weight2 = Weight2,
                        Weight3 = Weight3,
                    });
                }
            }

            return MyEarning;
        }

        public List<CorpEarning>GetApplicableWeighted(int CorpId)
        {
           
            var MyEarning = GetMyApplicableEarning(CorpId);
            foreach(var item in MyEarning)
            {

            }
            return null;
        }
       
    }
}
