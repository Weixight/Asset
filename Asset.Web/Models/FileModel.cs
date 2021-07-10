using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileSizeText { get; set; }
        public DateTime FileAccessed { get; set; }
        public string MyMethod { get; set; }
        public string MyKlass { get; set; }
    }
}
