using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Asset.Web.Data;
using OurHr.Models.AccountViewModels;
using OurHr.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asset.Web.Models.ManageViewModels;

namespace Asset.Web.Models.TamaViewModel
{
    public class HrCllection
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnv;

        public int AssetCount { get; set; }
        public string selTypeText { get; set; }
        public string selTypeRev { get; set; }
        public string Action { get; set; }
        public string Step { get; set; }
        //public AssetC Assetm { get; set; }
        public decimal MonVol { get; set; }
        public decimal TotalDeduct { get; set; }
        public string LevelApp { get; set; }
        public string ServiceRoot { get; set; }
        public decimal Gross { get; set; }
        public decimal Nepay { get; set; }
  

        // public PageTable1 PageTableOnlyy { get; set; }
        public ChangePasswordViewModel changePasswordViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }


        //  public ApplicationModules AppMode { get; set; }
        public RoleTbl MyRoleTbl { get; set; }
     
    
        public ChangePassVw MyPass { get; set; }
     
      
        public List<NewUser> NewUsersList { get; set; }
     
        // IEnumerable<NewStep> GetSteps { get; set; }
 




    }
}
