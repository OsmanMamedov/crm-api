using General.Api.Extensions;
using General.Business.Abstract;
using General.Business.Concrete;
using General.DataAccess;
using General.DataAccess.Abstract;
using General.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace General.Api
{
    public class Startup
    {
        private readonly IConfigurationRoot _appSettings;
        public IConfiguration Configuration { get; }

        #region Startup Cunstruction
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _appSettings = new ConfigurationBuilder()
          .SetBasePath(basePath: env.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }
        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            #region Logger
            //services.AddSingleton(typeof(ILogger), logger);

            #endregion
            #region AutoMapper
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapperProfiles());
            //});

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);
            #endregion
            #region DataAccess & Business DependencyInjection
            ScopedItems(services);
            #endregion
            #region Cors
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
                //.AllowCredentials();
                //.WithOrigins("http://localhost:4200");
            }));
            #endregion
            #region ReferenceLoopHandling
            services.AddMvc()
                        .AddNewtonsoftJson(options =>
                                           options.SerializerSettings.ReferenceLoopHandling =
                                           Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion
            #region ContextConnectionString
            GeneralContext.ConnectionString = _appSettings.GetConnectionString("GeneralContext");
            #endregion
            #region JWT Configuration
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services
                .AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = true,
                        ValidAudience = this.Configuration["Tokens:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = this.Configuration["Tokens:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            #endregion
            #region Controllers
            services.AddControllers();
            #endregion
            #region AddSwagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
            });
            #endregion
            #region MemoryCache
            //services.AddMemoryCache(); 
            #endregion
            #region HostedService
            //services.AddHostedService<ExampleService>();
            #endregion
            services.AddMvcCore();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            #region Development Env & Globale Exception
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseExceptionHandler("/Error");
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHsts();
            #endregion
            //app.UseHttpsRedirection();

            #region UseSwagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });
            #endregion
            #region Route
            app.UseRouting();
            #region Cors
            app.UseCors("CorsPolicy");
            #endregion
            #region .netCore 3.0 her ikiside olacak
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion
            app.UseEndpoints(endpoints =>
              {
                  endpoints.MapControllers();
              });
            #endregion
            app.UseStaticFiles();
        }
        #region DataAccess & Business DependencyInjection
        public static void ScopedItems(IServiceCollection services)
        {
            //User
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IUserRoleDal, EfUserRoleDal>();

            //Role
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IRoleDal, EfRoleDal>();

            //Company
            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddScoped<ICompanyDal, EfCompanyDal>();
        }
        #endregion
    }
}
