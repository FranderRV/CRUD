using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Login.Helpers
{
    public class Policy
    {

    }
    public class RoleAdmin : IAuthorizationRequirement
    {
        public string Role { set; get; }
        public RoleAdmin(string Role)
        {
            this.Role = Role;
        }

    }

    public class HandlerRoleAdmin : AuthorizationHandler<RoleAdmin>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAdmin requirement)
        {
          
            if (!context.User.HasClaim(x => x.Type == "Tipo"))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            else
            {
                string rol = context.User.FindFirst(x => x.Type == "Tipo").Value;

                if (rol == (requirement.Role))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail(); 
                }
            }
            return Task.CompletedTask;
        }
    }
}
