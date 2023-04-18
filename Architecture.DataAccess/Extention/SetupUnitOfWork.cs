using Architecture.DataAccess.Generic;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.Extention
{
    public static class SetupUnitOfWork
    {
        public static void SetupUnitOfWorkDA(this IServiceCollection services)
        {
            services.AddScoped(typeof(ISqlRepository<>), typeof(SqlDBRepository<>));

        }
    }
}
