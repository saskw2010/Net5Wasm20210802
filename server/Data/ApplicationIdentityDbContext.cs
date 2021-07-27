using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net5Wasm.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;

namespace Net5Wasm.Data
{
    public partial class ApplicationIdentityDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(
            DbContextOptions<ApplicationIdentityDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            this.OnModelBuilding(builder);
        }

    }
}
