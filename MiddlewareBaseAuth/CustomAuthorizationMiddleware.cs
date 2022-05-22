using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace MiddlewareBaseAuth
{
    public class CustomAuthorizationMiddleware:IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();
        public CustomAuthorizationMiddleware()
        {
            
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            var endpoint = context.GetEndpoint(); 
             
            if (endpoint != null)
            {
                var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                if (controllerActionDescriptor != null)
                {
                    var controller = controllerActionDescriptor.ControllerName;
                    var action = controllerActionDescriptor.ActionName;

                    if (authorizeResult.Succeeded)
                    {
                        if (action == "SecretForDev")
                        {
                            await context.ChallengeAsync();
                            return;
                        } 
                       
                    } 
                }
            }

            await defaultHandler.HandleAsync(next, context, policy, authorizeResult); 
        }
    }
}
