using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class RoleTbl
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDsc { get; set; }
    }
}
