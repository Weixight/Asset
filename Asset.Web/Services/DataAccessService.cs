using Asset.Web.Data;
using Asset.Web.Models;
using Asset.Web.Services;
using Microsoft.EntityFrameworkCore;
using OurHr.Models;
using OurHr.RoleAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OurHr.RoleAuthorization.Models
{
	public class DataAccessService : IDataAccessService
	{
		private readonly ApplicationDbContext _context;

		public DataAccessService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<NavigationMenu>> GetMenuItemsAsync(ClaimsPrincipal principal)
		{
			var isAuthenticated = principal.Identity.IsAuthenticated;
			if (!isAuthenticated)
				return new List<NavigationMenu>();

			var roleIds = await GetUserRoleIds(principal);
			var data = await (from menu in _context.AspNetRoleMenuPermission
							  where roleIds.Contains(menu.RoleId)
							  select menu)
							  .Select(m => new NavigationMenu()
							  {
								  Id = m.NavigationMenu.Id,
								  Name = m.NavigationMenu.Name,
								  Area = m.NavigationMenu.Area,
								  ActionName = m.NavigationMenu.ActionName,
								  ControllerName = m.NavigationMenu.ControllerName,
								  IsExternal = m.NavigationMenu.IsExternal,
								  ExternalUrl = m.NavigationMenu.ExternalUrl,
								  DisplayOrder = m.NavigationMenu.DisplayOrder,
								  ParentMenuId = m.NavigationMenu.ParentMenuId,
								  Visible = m.NavigationMenu.Visible,
								  Read = m.Read,
								  Update = m.Update,
								  Delete = m.Delete,
								  ServiceCode = m.ServiceCode,
								  // Table = m.Table
							  }).Distinct().ToListAsync();

			return data;
		}

		public async Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act)
		{
			var result = false;
			var roleIds = await GetUserRoleIds(ctx);
			var data = await (from menu in _context.AspNetRoleMenuPermission
							  where roleIds.Contains(menu.RoleId)
							  select menu)
							  .Select(m => m.NavigationMenu).Distinct().ToListAsync();

			foreach (var item in data)
			{
				result = (item.ControllerName == ctrl && item.ActionName == act);
				if (result)
					break;
			}

			return result;
		}

		public async Task<List<NavigationMenu>> GetPermissionsByRoleIdAsync(string id)
		{
			var items = await (from m in _context.AspNetNavigationMenu
							   join rm in _context.AspNetRoleMenuPermission
								on new { X1 = m.Id, X2 = id } equals new { X1 = rm.NavigationMenuId, X2 = rm.RoleId }
								into rmp
							   from rm in rmp.DefaultIfEmpty()
							   select new NavigationMenu()
							   {
								   Id = m.Id,
								   Name = m.Name,
								   Area = m.Area,
								   ActionName = m.ActionName,
								   ControllerName = m.ControllerName,
								   IsExternal = m.IsExternal,
								   ExternalUrl = m.ExternalUrl,
								   DisplayOrder = m.DisplayOrder,
								   ParentMenuId = m.ParentMenuId,
								   Visible = m.Visible,
								   Permitted = rm.RoleId == id,
								   Read = m.Read,
								   Update = m.Update,
								   Delete = m.Delete,
								   Table = m.Table,
								   ServiceCode = m.ServiceCode

							   })
							   .AsNoTracking()
							   .ToListAsync();

			return items;
		}

		public async Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<NavigationMenu> permissionIds)
		{
			try
			{
				var existing = await _context.AspNetRoleMenuPermission.Where(x => x.RoleId == id).ToListAsync();
				//foreach (var Myitem in existing)

				_context.RemoveRange(existing);


				_context.SaveChanges();
				foreach (var item in permissionIds)
				{
					if (item.ServiceCode == null)
					{
						item.ServiceCode = 0;
					}
					//var Mylist = _context.AspNetNavigationMenu.Where()
					var WherePermissionExist = _context.AspNetNavigationMenu.Where(k => k.Id == item.Id).ToList();
					await _context.AspNetRoleMenuPermission.AddAsync(new RoleMenuPermission()
					{
						RoleId = id,
						NavigationMenuId = item.Id,
						Read = item.Read,
						Name = WherePermissionExist.FirstOrDefault(k => k.Id == item.Id).Name,
						Update = item.Update,
						Delete = item.Delete,
						Create = item.Create,
						ServiceCode = (int)item.ServiceCode

					});

				}

				var result = await _context.SaveChangesAsync();

				return result > 0;
			}
			catch (Exception ex)
			{
				string message = ex.Message.ToString();
				return false;
			}
		}



		private async Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx)
		{
			var userId = GetUserId(ctx);
			var data = await (from role in _context.UserRoles
							  where role.UserId == userId
							  select role.RoleId).ToListAsync();

			return data;
		}

		private string GetUserId(ClaimsPrincipal user)
		{
			return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}

		public List<RoleMenuPermission> GetPermissionsByRoleIMaindAsync(string id)
		{
			var items = _context.AspNetRoleMenuPermission.Where(k => k.RoleId == id).ToList();





			//var items = (from m in _context.AspNetNavigationMenu
			//				  join rm in _context.AspNetRoleMenuPermission
			//				   on new { X1 = m.Id, X2 = id } equals new { X1 = rm.NavigationMenuId, X2 = rm.RoleId }
			//				   into rmp
			//				  from rm in rmp.DefaultIfEmpty()
			//				  select new RoleMenuPermission()
			//				  {
			//					  RoleId = id,
			//					  NavigationMenuId = m.Id,
			//					  Name = m.Name,
			//					  Read = m.Read,
			//					  Update = m.Update,
			//					  Delete = m.Delete,
			//					 // Table = m.Table
			//				  })
			//				   .AsNoTracking()
			//				   .ToListAsync();

			return items;
		}
	}
}