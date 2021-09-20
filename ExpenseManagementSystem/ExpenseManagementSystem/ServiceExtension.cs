using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagementSystem
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services, Account account, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.Add(new ServiceDescriptor(typeof(Cloudinary), c => new Cloudinary(account), lifetime));
            return services;
        }
        public static Account GetAccount(IConfiguration Configuration)
        {
            var cloudSettings = Configuration.GetSection("ImageUploadSettings");
            Account account = new(
              cloudSettings.GetSection("CloudName").Value,
              cloudSettings.GetSection("ApiKey").Value,
              cloudSettings.GetSection("ApiSecret").Value);
            return account;
        }
    }
}
