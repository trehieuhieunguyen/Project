using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Project.Data;
using Project.Helpers;
using Project.Models;
using Project.Repositorys;
using Project.Repositorys.IRepository;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;

namespace Project.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")),
                    ServiceLifetime.Transient);

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICoinRepository, CoinRepository>();
            services.AddScoped<IOrderCoinHeaderRepos, OrderCoinHeaderRepos>();
            services.AddScoped<IOrderCoinDetailRepos, OrderCoinDetailRepos>();
            services.AddScoped<ISaleCoinRepository, SaleCoinRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IDashBoardRespository, DashBoardRerpository>();
            var mailkit = Configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config =>
                config.UseMailKit(mailkit));

            var accountSid = Configuration["Twilio:AccountSID"];
            var authToken = Configuration["Twilio:AuthToken"];
            TwilioClient.Init(accountSid, authToken);

            services.Configure<TwilioVerifySettings>(Configuration.GetSection("Twilio"));

            services.AddAutoMapper(typeof(Mappings).Assembly);
            return services;
        }
    }
}
