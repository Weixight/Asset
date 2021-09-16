using Asset.Web.Data;
using Asset.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.ViewModels
{
    
    public class MyEarningList
    {
        private readonly ApplicationDbContext _db;
        public MyEarningList(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<CorpEarning> TheEarningList(int id)
        {
            List<CorpEarning> NewEarning = new List<CorpEarning>();
            var MyItemList = _db.AverageMaintanableEarningsWeighteds.ToList();
            var MyEarning = _db.MyEarning.Where(K => K.Corpid == id).ToList();
            var CorpReg = _db.corpRegs.FirstOrDefault(K => K.id == id);
            var EarningSetUp = _db.OurCorpEarningSetup.Where(K => K.Applied == true && K.Corpid == id).ToList();
            if (EarningSetUp.Count != 0)
            {
                foreach (var item in EarningSetUp)
                {
                    
                    var TheAverageEarning = MyEarning.FirstOrDefault(K => K.CorpEarningid == item.CorpEarningid);
                   
                        NewEarning.Add(new CorpEarning
                        {
                            id = item.id,
                            CalculatedItem = item.CorpEarning.CalculatedItem,
                            Corpid = item.Corpid,
                            CorpReg = CorpReg,
                            CorpEarningid = item.CorpEarningid,
                            KorpEarning = item,


                        });
                   
                   
                }
            }
            else
            {

            }
            return NewEarning;
        }

        public int Create(List<CorpEarning> CorpIncome)
        {
            var MyIncomeItem = CreateIncomeList(CorpIncome);
            var CorpReg = _db.corpRegs.FirstOrDefault(K => K.id == MyIncomeItem.FirstOrDefault().Corpid);
            var EarningKop = _db.OurCorpEarningSetup.FirstOrDefault(K => K.CorpEarningid == MyIncomeItem.FirstOrDefault().CorpEarningid);
            foreach (var item in MyIncomeItem)
            {
                _db.MyEarning.Add(new CorpEarning
                {
                    CorpReg = CorpReg,
                    KorpEarning = EarningKop,
                    CalculatedItem = item.CalculatedItem,
                    CorpEarningid = item.CorpEarningid,
                    Corpid = item.Corpid,
                    ValueAmount = item.ValueAmount,
                    ValueDate = item.ValueDate
                }) ;
            }
            var result = _db.SaveChanges(); 
            return result;
        }

        private List<CorpEarning> CreateIncomeList(List<CorpEarning> CorpIncome)
        {
            
            List<CorpEarning> MyEarning = new List<CorpEarning>();
            List<decimal> MyValue = new List<decimal>();
            List<DateTime> MyDate = new List<DateTime>();
          
            int Number = 0;
            foreach(var item in CorpIncome)
            {
                MyValue.Add(item.Year_One);
                MyValue.Add(item.Year_Two);
                MyValue.Add(item.Year_Three);
                MyDate.Add(item.Date_One);
                MyDate.Add(item.Date_Two);
                MyDate.Add(item.Date_Three);

                MyEarning.Add(new CorpEarning
                {
                     CalculatedItem = item.CalculatedItem,
                     CorpEarningid = item.CorpEarningid,
                     Corpid        = item.Corpid,
                     CorpReg = item.CorpReg,
                     KorpEarning = item.KorpEarning,
                     ValueAmount = MyValue[Number],
                     ValueDate = MyDate[Number]
                     
                });
                Number++;
            }
            return MyEarning;
        }
    }
}
