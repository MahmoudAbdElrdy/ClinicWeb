using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.IRepository;
using ApplicationCore.Repository;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Services.Mapper;
using Services.IServices;
using Services.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Services;

namespace ClinicWeb
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
            //services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
          //  services.AddSwaggerGenNewtonsoftSupport();
            services.AddDbContext<ApiContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:ClinicDB"]));

      // For Identity  
      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApiContext>()
          .AddDefaultTokenProviders();
      // Adding Authentication  
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })

      // Adding Jwt Bearer  
      .AddJwtBearer(options =>
      {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = Configuration["JWT:ValidAudience"],
          ValidIssuer = Configuration["JWT:ValidIssuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
        };
      });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "project Api", Version = "v1" });
                //-----------------------------start jwtSettings swagger ---------------------------------
        //        var security = new Dictionary<string, IEnumerable<string>>
        //        {
        //            {"Bearer",new string[0]}

        //        };
        //        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Description =
        //          "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        //            Name = "Authorization",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer"
        //        });
        //        x.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type = ReferenceType.SecurityScheme,
        //                Id = "Bearer"
        //            },
        //            Scheme = "oauth2",
        //            Name = "Bearer",
        //            In = ParameterLocation.Header,

        //        },
        //        new List<string>()
        //    }
        //});
                //-----------------------------end jwtSettings swagger---------------------------------

            });// Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepository<>));
              services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));
              services.AddScoped(typeof(IIdentityRepository<>), typeof(IdentityRepository<>));
               services.AddScoped<IUnitOfWork, UnitOfWork>();
              services.AddScoped<IResponseDTO, ResponseDTO>();
              services.AddTransient<IServicesClinic, ServicesClinic>();
              services.AddTransient<IServicesDoctor, ServicesDoctor>();
              services.AddTransient<IServicesItems, ServicesItems>();
              services.AddTransient<IServicesPatient, ServicesPatient>();
              services.AddTransient<IServicesStore, ServicesStore>();
          
          
            services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddFile("Logs/log-{Date}.txt");
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
            //--------------swagger configuration------------------
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " Auditor V1");
                c.DocExpansion(DocExpansion.None);

            });
            app.UseRouting();
      app.UseCors("CorsPolicy");
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
