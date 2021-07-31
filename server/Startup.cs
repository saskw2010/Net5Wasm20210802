using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Net5Wasm.Data;
using Net5Wasm.Models;
using Net5Wasm.Authentication;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
namespace Net5Wasm
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAny",
                    x =>
                    {
                        x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials();
                    });
            });
            services.AddOData();
            services.AddODataQueryFilter();

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("net5wasmconnConnection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddRoleStore<RoleStore<IdentityRole, ApplicationIdentityDbContext, string>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();

            services.AddTransient<IdentityServer4.Services.IProfileService, ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddDbContext<Net5Wasm.Data.Net5WasmconnContext>(options =>
            {
              options.UseSqlServer(Configuration.GetConnectionString("net5wasmconnConnection"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddLocalization();

            var supportedCultures = new[]
            {
                new System.Globalization.CultureInfo("ar-KW"),
                new System.Globalization.CultureInfo("en-US"),
                new System.Globalization.CultureInfo("fr-CA"),
                new System.Globalization.CultureInfo("it-IT"),
                new System.Globalization.CultureInfo("es-ES"),
                new System.Globalization.CultureInfo("zh-CHS"),
                new System.Globalization.CultureInfo("tr-TR"),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfigureOData(ODataConventionModelBuilder builder);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationIdentityDbContext identityDbContext)
        {
            OnConfiguring(app, env);

            var supportedCultures = new[]
            {
                new System.Globalization.CultureInfo("ar-KW"),
                new System.Globalization.CultureInfo("en-US"),
                new System.Globalization.CultureInfo("fr-CA"),
                new System.Globalization.CultureInfo("it-IT"),
                new System.Globalization.CultureInfo("es-ES"),
                new System.Globalization.CultureInfo("zh-CHS"),
                new System.Globalization.CultureInfo("tr-TR"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    ctx.Request.Scheme = "https";
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
            app.UseCors("AllowAny");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.Count().Filter().OrderBy().Expand().Select().MaxTop(null).SetTimeZoneInfo(TimeZoneInfo.Utc);

                var oDataBuilder = new ODataConventionModelBuilder(provider);

                oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.Address>("Addresses");
                oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.Category>("Categories");
                oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.Order>("Orders");

                var ordersProduct = oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>("OrdersProducts");
                ordersProduct.EntityType.HasKey(entity => new {
                  entity.OrderId, entity.ProductId
                });
                oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.Product>("Products");
                oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.ShoppingCart>("ShoppingCarts");

                var shoppingCartsProduct = oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>("ShoppingCartsProducts");
                shoppingCartsProduct.EntityType.HasKey(entity => new {
                  entity.ShoppingCartId, entity.ProductId
                });
                oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.Wishlist>("Wishlists");

                var wishlistsProduct = oDataBuilder.EntitySet<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>("WishlistsProducts");
                wishlistsProduct.EntityType.HasKey(entity => new {
                  entity.WishlistId, entity.ProductId
                });

                this.OnConfigureOData(oDataBuilder);

                oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
                var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
                usersType.AddCollectionProperty(typeof(ApplicationUser).GetProperty("RoleNames"));
                oDataBuilder.EntitySet<IdentityRole>("ApplicationRoles");
                var model = oDataBuilder.GetEdmModel();

                endpoints.MapODataRoute("odata", "odata/net5wasmconn", model);

                endpoints.MapODataRoute("auth", "auth", model);
            });

            identityDbContext.Database.Migrate();

            OnConfigure(app, env);
        }
    }


    public class ProfileService : IdentityServer4.Services.IProfileService
    {
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.env = env;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(IdentityServer4.Models.ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            var roles = user != null ? await userManager.GetRolesAsync(user) :
                env.IsDevelopment() && context.Subject.Identity.Name == "admin" ?
                    roleManager.Roles.Select(r => r.Name) : Enumerable.Empty<string>();

            context.IssuedClaims.AddRange(roles.Select(r => new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Role, r)));
        }

        public Task IsActiveAsync(IdentityServer4.Models.IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
