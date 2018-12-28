using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebshopApp.Data;
using WebshopApp.Data.Common;
using WebshopApp.Models;
using WebshopApp.Services.DataServices;
using WebshopApp.Services.DataServices.Contracts;
using WebshopApp.Services.MappingServices;
using WebshopApp.Services.Models.ViewModels;
using WebshopApp.Web.Models;

namespace WebshopApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ProductViewModel).Assembly,
                typeof(ProductsCollectionViewModel).Assembly
            );

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WebshopAppContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            
            services.AddIdentity<WebShopUser, IdentityRole>(op =>
                {
                    op.Password.RequireDigit = false;
                    op.Password.RequiredLength = 3;
                    op.Password.RequireUppercase = false;
                    op.Password.RequireNonAlphanumeric = false;
                    op.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<WebshopAppContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    facebookOptions.Validate("Facebook");
                    facebookOptions.Events = new OAuthEvents
                    {
                        OnRemoteFailure = ctx =>
                        {
                            ctx.Response.Redirect("/Identity/Error?ErrorMessage=" + UrlEncoder.Default.Encode(ctx.Failure.Message));
                            ctx.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = new TimeSpan(0, 4, 0, 0);
                options.Cookie.HttpOnly = true;
            });

            services.AddLogging(lb =>
            {
                lb.AddConfiguration(Configuration.GetSection("Logging"));
                lb.AddFile(o => o.RootPath = AppContext.BaseDirectory);
            });

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IBlogsService, BlogsService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IReceiptsService, ReceiptsService>();
            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<ICartsService, CartsService>();
            
            services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebshopAppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //Seeder.Seed(context);
            //Seeder.SeedPictures(context);
            //Seeder.SeedRoles(context);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
