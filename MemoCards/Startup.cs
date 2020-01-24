using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MemoCards.Data;
using MemoCards.Mapper;
using MemoCards.Models;
using MemoCards.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace MemoCards
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();

            services.AddSingleton(provider => MapperConfig.Initialize());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMemoCardService, MemoCardService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var id = context
                                .Principal
                                .Claims
                                .First(claim => claim.Type == nameof(User.Id))
                                .Value;

                            var service = context
                                .HttpContext
                                .RequestServices
                                .GetRequiredService<IUserService>();

                            var user = await service.GetUser(Guid.Parse(id));

                            if (user == null) context.Fail("Unauthorized");

                            context.HttpContext.Items.Add(nameof(User), user); // Can be used by actions
                        }
                    };
                    var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = nameof(MemoCards),
                        ValidAudience = nameof(MemoCards),
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Static", "css")),
                RequestPath = "/static/css"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider =
                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Static", "lang")),
                RequestPath = "/static/xml"
            });

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
//            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "static",
                    "static/{action}/{filename}",
                    defaults: new {controller = "File"});

                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment()) spa.UseReactDevelopmentServer("start");
            });
        }
    }
}