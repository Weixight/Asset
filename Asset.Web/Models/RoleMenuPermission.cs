using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class RoleMenuPermission
    {
        [Key]
        public string RoleId { get; set; }

        public Guid NavigationMenuId { get; set; }
       // [NotMapped]
        public string Name { get; set; }
        public NavigationMenu NavigationMenu { get; set; }
        public int ServiceCode { get;set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
