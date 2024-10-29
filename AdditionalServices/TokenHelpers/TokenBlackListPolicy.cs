using Microsoft.AspNetCore.Authorization;
using webNET_2024_aspnet_1.DBContext;

namespace webNET_2024_aspnet_1.AdditionalServices.TokenHelpers
{

        public class TokenBlackListPolicy : AuthorizationHandler<TokenBlackListRequirment>
        {
            private readonly IServiceProvider _serviceProvider;

            public TokenBlackListPolicy(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenBlackListRequirment requirement)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                    string authorizationHeader = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                    {
                        var token = authorizationHeader.Substring("Bearer ".Length);
                        Console.WriteLine(token);
                        var blackToken = appDbContext.BlackTokens.FirstOrDefault(b => b.Blacktoken == token);

                        if (blackToken != null)
                        {
                            context.Fail();
                        }
                        else
                        {
                            context.Succeed(requirement);
                        }
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
