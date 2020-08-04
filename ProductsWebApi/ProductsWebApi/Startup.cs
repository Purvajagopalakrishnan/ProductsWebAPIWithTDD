using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsWebApi.DBContext;
using ProductsWebApi.Interface;
using ProductsWebApi.Repository;
using ProductsWebApi.Services;
using ProductsWebApi.Services.AuthenticationServices;
using ProductsWebApi.Validator;
using ProductsWebAPI.Repository;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;

namespace ProductsWebApi
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
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserValidator, UserValidator>();

            services.AddDbContext<ProductsDBContext>(options =>
     options.UseSqlServer((Configuration.GetConnectionString("SqlConnectionString"))));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = SecuritySchemeType.ApiKey });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "jwt",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                            new List<string>()
                    }
                });

                string secretKey = Configuration.GetSection("JWTParameter:SecretKey").Value;

                var jettokenparams = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetSection("JWTParameter:Issuer").Value,
                    ValidAudience = Configuration.GetSection("JWTParameter:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwtconfig =>
                        jwtconfig.TokenValidationParameters = jettokenparams);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
}
}