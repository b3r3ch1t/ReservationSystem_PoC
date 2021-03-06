﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ReservationSystem_PoC.API.Configurations
{
    internal static class SwaggerConfig
    {
        internal static void AddSwaggerConfiguration(this IServiceCollection services)
        {

            //https://github.com/domaindrivendev/Swashbuckle.AspNetCore
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Reservation System - POC",
                    Description = "Very Samll Reservation - POC",
                    Contact = new OpenApiContact
                    {
                        Name = "Anderson Meneses",
                        Email = "anderson.meneses@gmail.com"
                    },

                });


            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}