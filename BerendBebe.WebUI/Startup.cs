using AutoMapper;
using BerendBebe.Business.Abstract;
using BerendBebe.Business.Concrete;
using BerendBebe.Business.ValidationRules.FluentValidatiors;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.DataAccess.Concrete.EntityFrameork.Context;
using BerendBebe.DataAccess.Concrete.EntityFrameork.Repository;
using BerendBebe.Entities.Concrete;
using BerendBebe.MvcHelpers.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerendBebe.WebUI
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
            services.AddSession();
            services.AddDbContext<BerendBebeContext>();



            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<BerendBebeContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Admin/Account/SignIn/");
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "BerendBebe";
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
                opt.SlidingExpiration = true;
            });

            services.AddControllersWithViews()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining(typeof(CategoryAddValidator));
                    config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddHttpContextAccessor();

            services.AddScoped(typeof(IBaseDal<>), typeof(EfBaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseManager<>));

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductRepository>();

            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IProductImageDal, EfProductImageRepository>();

            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<ICartDal, EfCartRepository>();

            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDal, EfOrderRepository>();

            services.AddScoped<IPaymentTypeService, PaymentTypeManager>();
            services.AddScoped<IPaymentTypeDal, EfPaymentTypeRepository>();

            services.AddScoped<IOrderDetailService, OrderDetailManager>();
            services.AddScoped<IOrderDetailDal, EfOrderDetailRepository>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentRepository>();

            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, EfContactRepository>();

            services.AddScoped<IAccountService, AccountManager>();

            services.AddAutoMapper(typeof(Startup));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ActionResultExtensions.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>(),
                app.ApplicationServices.GetRequiredService<ITempDataDictionaryFactory>());
        }
    }
}
