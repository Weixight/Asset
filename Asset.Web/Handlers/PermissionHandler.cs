using Asset.Web.Data;
using Asset.Web.Models;
using Asset.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asset.Web.Handlers
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        private string policyName;
		private readonly IHttpContextAccessor _httpContextAccessor; 

		public AuthorizationRequirement()
        {
			//_httpContextAccessor = httpContextAccessor;
        }

        public AuthorizationRequirement(string policyName)
        {
            this.policyName = policyName;
        }
    }

    public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
	{
		private readonly IDataAccessService _dataAccessService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;


		public PermissionHandler(IDataAccessService dataAccessService, IHttpContextAccessor httpContextAccessor,RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
		{
			_dataAccessService = dataAccessService;
			_httpContextAccessor = httpContextAccessor;
			_roleManager = roleManager;
			_context = applicationDbContext;
		}

		protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
		{
			if (context.Resource is RouteEndpoint endpoint)
			{
				endpoint.RoutePattern.RequiredValues.TryGetValue("controller", out var _controller);
				endpoint.RoutePattern.RequiredValues.TryGetValue("action", out var _action);
				List<RoleMenuPermission> MyPermission = new List<RoleMenuPermission>();
				var UserMail =  context.User.Identity;
				//var RoleUser = context.User.Claims.
				var roles=context.User.Claims.Where(c => c.Type == ClaimTypes.Role);
				
				
				//foreach (var item in )
				//roles.Any();
				

				//var username = context.User.Claims.w((System.Security.Claims.ClaimTypes.NameIdentifier.ToString()));
				//var claims = username.Subject.Claims.Where( k);
				
				//var userRole = username.
				//var MyPagePermission = _dataAccessService.GetPermissionsByRoleIdAsync(context.User.);

				endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
				endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);
				var MyNaf = _context.AspNetNavigationMenu.ToList();
				var isAuthenticated = context.User.Identity.IsAuthenticated;

				if (isAuthenticated && _controller != null && _action != null && await _dataAccessService.GetMenuItemsAsync(context.User, _controller.ToString(), _action.ToString()))
				{
					foreach (var item in roles)
					{
						var MyRoles = await _roleManager.FindByNameAsync(item.Value);
						var RolePermission = _dataAccessService.GetPermissionsByRoleIMaindAsync(MyRoles.Id);
						foreach(var MyPer in RolePermission)
                        {
							var Myurl = MyNaf.FirstOrDefault(k => k.ParentMenuId == MyPer.NavigationMenuId);
							if (Myurl != null )
							{
								if (MyPer.Read == true )
								{
									context.Succeed(requirement);
								}
								else
								{
									context.Fail();
								}
								if (MyPer.Update == true )
								{
									context.Succeed(requirement);
								}
								else
								{
									context.Fail();
								}
                                if (MyPer.Delete == true )
                                {
                                    context.Succeed(requirement);
                                }
                                else
                                {
                                    context.Fail();
                                }
                                if (MyPer.Create == true )
								{
									context.Succeed(requirement);
								}

								else
								{
									context.Fail();
								}
							}
                            else
                            {
								//context.Fail;
							}

						}
					}
					
					
				}
			}
		}

	
	}
}