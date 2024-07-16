using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication
{
    public static class JwtRegistration
    {
        public static void RegisterCustomJwt(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {

            })
        }
    }
}
