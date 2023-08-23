using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class IsGoodDeleteHandler: AuthorizationHandler<IsGoodsDeleteRequirement, Goods>
	{
        private readonly UserManager<User> _user;
        private readonly ILogger<IsGoodDeleteHandler> logger;

        public IsGoodDeleteHandler(UserManager<User> user, ILogger<IsGoodDeleteHandler> logger)
		{
            _user = user;
            this.logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsGoodsDeleteRequirement requirement, Goods resource)
        {
            var appUser = await _user.GetUserAsync(context.User);
            logger.LogInformation("User {appUser} try access", appUser.Id);
            if (appUser == null)
            {
                
                return;
            }
            if (resource.User.Id == appUser.Id)
            {
                context.Succeed(requirement);
            }
        }
    }
}

