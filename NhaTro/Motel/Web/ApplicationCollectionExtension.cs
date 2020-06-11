﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web
{
    public static class ApplicationCollectionExtension
    {
        public static void Build(this IApplicationBuilder application, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=DangNhap}/{action=Login}");

                routes.MapSpaFallbackRoute(
                     name: "spa-fallback",
                     defaults: new { controller = "DangNhap", action = "Login" });
            });
        }
    }
}
