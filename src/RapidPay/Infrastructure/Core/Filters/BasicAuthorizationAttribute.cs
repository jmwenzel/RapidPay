using Microsoft.AspNetCore.Authorization;

namespace RapidPay.Infrastructure.Core.Filters
{
    /// <summary>
    /// Basic Authorization filter
    /// </summary>
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BasicAuthorizationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
