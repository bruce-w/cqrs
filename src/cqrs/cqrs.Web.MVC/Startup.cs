﻿using AutoMapper;
using cqrs.CommandStack.CommandHandlers;
using cqrs.CommandStack.Commands;
using cqrs.Data.Sql.EF;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.InMemory;
using cqrs.Messaging.Interfaces;
using cqrs.Web.MVC.Middleware;
using cqrs.Web.MVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace cqrs.Web.MVC
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
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AuctionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.AddMvc();

            services.AddScoped<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandler<CreateAuctionCommand>, CreateAuctionCommandHandler>();
            services.AddScoped<ICommandHandler<CloseAuctionCommand>, CloseAuctionCommandHandler>();
            services.AddScoped<ICommandHandler<CancelAuctionCommand>, CancelAuctionCommandHandler>();
            services.AddScoped<ICommandHandler<BidCommand>, BidCommandHandler>();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //todo: app.UseExceptionHandler
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Auction}/{action=Index}/{id?}");
            });
        }
    }
}
