using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class NavigationMenu
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string? Name { get; set; }

		[ForeignKey("ParentNavigationMenu")]
		public Guid? ParentMenuId { get; set; }

		public virtual NavigationMenu ParentNavigationMenu { get; set; }

		public string? Area { get; set; }
		public string? Klass { get; set; }

		public string? ControllerName { get; set; }

		public string? ActionName { get; set; }
		public Nullable<int> ServiceCode { get; set; }
		public bool IsExternal { get; set; }

		public string? ExternalUrl { get; set; }

		public Nullable<int> DisplayOrder { get; set; }

		[NotMapped]
		public bool Permitted { get; set; }

		public bool Visible { get; set; }
		public bool Create { get; set; }
		public bool Delete { get; set; }
		public bool Update { get; set; } 
		public bool Read { get; set; }
		public bool Table { get; set; }
	}
}
