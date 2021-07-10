using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Asset.Web.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
   
    public class state
    {
        [Key]
        public int state_id { get; set; }
        public string state_name { get; set; }
       // public ICollection<lga> lga { get; set; }
    }

    public class lga
    {
        [Key]
        public int lga_id { get; set; }
        public string lga_name { get; set; }
        public int state_id { get; set; }
    }

    //public class locals
    //{

    //    public int id { get; set; }
    //    public string name { get; set; }
    //    //public string State { get; set; }
    //}
    //public class statelga
    //{
        
    //    public string name { get; set; }
    //    public string id { get; set; }
    //    public string locals { get; set; }
        

    //}

    //public class RegionalData
    //{
    //    [Obsolete]
    //    private readonly IHostingEnvironment _hostingEnvironment;

    //    [Obsolete]
    //    public RegionalData(IHostingEnvironment hostingEnvironment)
    //    {
    //        _hostingEnvironment = hostingEnvironment;
    //    }

    //    //[Obsolete]
    //    //public RegionalData(IHostingEnvironment hostingEnv)
    //    //{
    //    //    _hostingEnvironment = hostingEnv;
    //    //    //_signInManager = userManager;
    //    //}
    //    [Obsolete]
    //    public List<statelga> MyStateList()
    //    {
    //        //string path = Server.MapPath("TrackData/vehicle_points.txt");
    //        //StreamReader reader = File.OpenText(path);
    //        var owners = System.IO.File.ReadAllLines(System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "gistfile1.txt")).ToString();
    //         string json = File.ReadAllText("gistfile1.txt");
            
            
    //        var StateList = JsonConvert.DeserializeObject(json);
    //        JArray jsonResponse = JArray.Parse(StateList.ToString());

    //        foreach (var item in jsonResponse)
    //        {
    //            JObject jRaces = (JObject)item["locals"];
    //            foreach (var rItem in jRaces)
    //            {
    //                string rItemKey = rItem.Key;
    //                JObject rItemValueJson = (JObject)rItem.Value;
    //               // Races rowsResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Races>(rItemValueJson.ToString());
    //            }
    //        }
    //        var StateListm = JsonConvert.DeserializeObject<List<state>>((string)StateList);
    //        return null;
    //    }

    //}

   




}
