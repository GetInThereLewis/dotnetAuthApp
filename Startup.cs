using Microsoft.EntityFrameworkCore;
using dotnetAuthApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using dotnetAuthApp.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net.Http;
namespace dotnetAuthApp {

    public class Startup {
        public Startup(IConfiguration _configuration) {
            Configuration = _configuration;
        }
        public IConfiguration Configuration{get;}

        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
                };
            });
            services.AddControllers();
            services.AddRazorPages();
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HW API", Description = "none", Version = "1.0" });

                // options.DocInclusionPredicate((docName, apiDesc) => {
                //     if(apiDesc.ActionDescriptor is ControllerActionDescriptor actionDescriptor) {
                //         return actionDescriptor.MethodInfo.GetCustomAttributes(typeof(HttpMethodAttributes), inherit: true).Any();
                //     }
                //     return false;
                // });
            });
         
            services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={Configuration.GetConnectionString("DefaultConnection")}"));
            // services.AddControllersWithViews();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            } else {
                //
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapGet("/quatsch", async context => {
                await context.Response.WriteAsync("Hallo");
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI( c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HW API V1");
            });
            
        }
    }

}
    // public class UserDb : DbContext {
    //     public string DbPath{get; set;}
    //     public DbSet<ExerciseModel> Exercises {get; set;}
    //     public DbSet<UserModel> Users {get; set;}
    //     public void UserExerciseContext() {
    //         var folder = Environment.SpecialFolder.LocalApplicationData;
    //         var path = Environment.GetFolderPath(folder);
    //         DbPath = System.IO.Path.Join(path, "hw.db");
    //     }
    //     protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");

    // }

