using Asset.Web.Data;
using Asset.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.ViewModels
{

    public class MydateValue
    {
       public  DateTime MyDate { get; set; }
       public decimal MyValue { get; set; }
        public int MyEarningid { get; set; }
        public int CorpId { get; set; }

    }

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
                    var ItemList = MyItemList.FirstOrDefault(K => K.id == item.CorpEarningid);
                    var TheAverageEarning = MyEarning.FirstOrDefault(K => K.CorpEarningid == item.CorpEarningid);
                   
                        NewEarning.Add(new CorpEarning
                        {
                            id = item.id,
                            CalculatedItem = item.CorpEarning.CalculatedItem,
                            Corpid = item.Corpid,
                            CorpReg = CorpReg,
                            CorpEarningid = item.id,
                            KorpEarning = item,
                            Purpose = ItemList.Purpose,


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
            var EarningKop = _db.OurCorpEarningSetup.ToList();
            foreach (var item in MyIncomeItem)
            {
                var MySetUp = EarningKop.FirstOrDefault(l => l.id == item.CorpEarningid);
                _db.MyEarning.Add(new CorpEarning
                {
                    CorpReg = CorpReg,
                    KorpEarning = MySetUp,
                    CalculatedItem = MySetUp.CalculatedItemName,
                    CorpEarningid = MySetUp.id,
                    Corpid = item.Corpid,
                    ValueAmount = item.ValueAmount,
                    ValueDate = item.ValueDate
                }) ;
            }
            var result = _db.SaveChanges(); 
            return result;
        }

        public int CreateSingleEarning(List<CorpEarning> CorpIncome, DateTime ValueDate)
        {
            var MyEarning = _db.MyEarning.Where(K => K.Corpid == CorpIncome.FirstOrDefault().Corpid);
            var CorpReg = _db.corpRegs.FirstOrDefault(K => K.id == CorpIncome.FirstOrDefault().Corpid);
            var EarningKop = _db.OurCorpEarningSetup.ToList();
            foreach (var item in CorpIncome)
            {
                var MyEarningExist = MyEarning.FirstOrDefault(K => K.ValueDate == item.ValueDate && K.CorpEarningid == item.CorpEarningid);
                if (item.ValueDate != ValueDate && item.CorpEarningid == MyEarningExist.CorpEarningid)
                {
                    var MySetUp = EarningKop.FirstOrDefault(l => l.id == item.CorpEarningid);
                    _db.MyEarning.Add(new CorpEarning
                    {
                        CorpReg = CorpReg,
                        KorpEarning = MySetUp,
                        CalculatedItem = MySetUp.CalculatedItemName,
                        CorpEarningid = MySetUp.id,
                        Corpid = item.Corpid,
                        ValueAmount = item.ValueAmount,
                        ValueDate = item.ValueDate
                    });
                }
            }
            var result = _db.SaveChanges();
            return result;
        }

        private List<CorpEarning> CreateIncomeList(List<CorpEarning> CorpIncome)
        {
            
            List<CorpEarning> MyEarning = new List<CorpEarning>();
            List<MydateValue> MyValue = new List<MydateValue>();
           
          
            int Number = 0;
            foreach (var item in CorpIncome)
            {
                MyValue.Add(new MydateValue
                {
                    MyDate = item.Date_One,
                    MyValue = item.Year_One,
                    MyEarningid = item.CorpEarningid,
                    CorpId = item.Corpid
                });
                MyValue.Add(new MydateValue
                {
                    MyDate = item.Date_Two,
                    MyValue = item.Year_Two,
                    MyEarningid = item.CorpEarningid,
                     CorpId = item.Corpid
                });
                MyValue.Add(new MydateValue
                {
                    MyDate = item.Date_Three,
                    MyValue = item.Year_Three,
                    MyEarningid = item.CorpEarningid,
                     CorpId = item.Corpid
                });
            }
                foreach (var itemValuedate in MyValue)
                {
                    MyEarning.Add(new CorpEarning
                    {
                       
                        ValueAmount = itemValuedate.MyValue,
                        ValueDate = itemValuedate.MyDate,
                        CorpEarningid = itemValuedate.MyEarningid,
                        Corpid = itemValuedate.CorpId
                       
                         

                    });
                    Number++;
                }

               
            
            return MyEarning;
        }
    }
}
