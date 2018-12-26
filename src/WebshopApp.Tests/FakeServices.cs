using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebshopApp.Data;
using WebshopApp.Models;
using WebshopApp.Services.DataServices.Contracts;

namespace WebshopApp.Services.DataServices.Tests
{
    public class FakeServices
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected WebshopAppContext Context { get; set; }
        
        public FakeServices(IServiceProvider serviceProvider, WebshopAppContext context)
        {
            ServiceProvider = serviceProvider;
            this.Context = context;
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<WebshopAppContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid()
                    .ToString()));

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IBlogsService, BlogsService>();

            services.AddIdentity<WebShopUser, IdentityRole>()
                .AddEntityFrameworkStores<WebshopAppContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            return services;
        }
    }
}
