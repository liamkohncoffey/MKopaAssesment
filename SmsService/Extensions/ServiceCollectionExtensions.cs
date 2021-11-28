using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SmsService.Definition.Core.Exceptions;
using SmsService.Domain.contracts;
using SmsService.Integration.Handlers;
using SmsService.SenderOne;
using SmsService.SenderTwo;

namespace SmsService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SendSmsCommandConsumer>();
                
                //This can be configured to support any message broker
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context, configurator =>
                    {
                        cfg.UseMessageRetry(r =>
                            {
                                r.Interval(2, TimeSpan.FromMinutes(3));
                                r.Ignore(typeof(NotImplementedException));
                                r.Ignore(typeof(ValidationException));
                            }
                        );
                        cfg.ReceiveEndpoint("SmsService",
                            e => { e.ConfigureConsumer<SendSmsCommandConsumer>(context); });
                    });
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }

        public static IServiceCollection AddSmsProviders(this IServiceCollection services)
        {
            services.AddScoped<ISmsService, SenderTwoSmsService>();
            services.AddScoped<ISmsService, SenderOneSmsService>();
            
            return services;
        }
    }
}