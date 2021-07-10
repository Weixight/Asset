using OurHr.RoleAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Asset.Web.Models;

namespace Asset.Web.Services
{
	public interface IDataAccessService
	{
		Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act);
		Task<List<NavigationMenu>> GetMenuItemsAsync(ClaimsPrincipal principal);
		Task<List<NavigationMenu>> GetPermissionsByRoleIdAsync(string id);
		 List<RoleMenuPermission> GetPermissionsByRoleIMaindAsync(string id);
		Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<NavigationMenu> permissionIds);
	}
}