using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Queries;
using Motel.Repositories;
using Rotativa.AspNetCore;
using Web;

namespace Motel
{
    public class Startup
    {

        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IHostingEnvironment env)
        {

            ConfigurationRoot = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(ConfigurationRoot.GetConnectionString("DefaultConnectionString")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>();
            services.AddMvc();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddHttpContextAccessor();
            services.AddTransient<IKhachHangRepository, KhachHangRepository>();
            services.AddTransient<IHomeRepository, HomeRepository>();
            services.AddTransient<ITaiKhoanRepository, TaiKhoanRepository>();
            services.AddTransient<INhaTroRepository, NhaTroRepository>();
            services.AddTransient<IPhongRepository, PhongRepository>();
            services.AddTransient<IDatPhongRepository, DatPhongRepository>();
            services.AddTransient<IHoaDonRepository, HoaDonRepository>();
            services.AddTransient<IHopDongRepository, HopDongRepository>();
            services.AddTransient<IPhuongTienRepository, PhuongTienRepository>();
            services.AddTransient<IDichVuRepository, DichVuRepository>();
            services.AddTransient<ILoaiPhongRepository, LoaiPhongRepository>();
            services.AddTransient<IDonViTinhRepository, DonViTinhRepository>();
            services.AddTransient<ILoaiDichVuRepository, LoaiDichVuRepository>();
            services.AddTransient<IChuTroRepository, ChuTroRepository>();
            services.AddTransient<IDichVuPhongRepository, DichVuPhongRepository>();
            services.AddTransient<INhomNguoiDungRepository, NhomNguoiDungRepository>();
            services.AddTransient<IDienNuocRepository, DienNuocRepository>();
            services.AddTransient<IPhanQuyenRepository, PhanQuyenRepository>();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory looger)
        {

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseIdentity();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=DangNhap}/{action=Login}/{id?}");

                routes.MapSpaFallbackRoute(
                     name: "spa-fallback",
                     defaults: new { controller = "NhaTro", action = "Index" });

            });
            RotativaConfiguration.Setup(env);


        }
    }
}