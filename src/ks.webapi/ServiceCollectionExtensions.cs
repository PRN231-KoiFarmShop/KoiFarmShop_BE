using FluentValidation;
using ks.application.MapperProfiles;
using ks.infras;
using ks.webapi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scrutor;
using System.Reflection;
using System.Text;

namespace ks.webapi;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(opt =>
                    {
                        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Koi-Shipment", Version = "v1" });
                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        opt.IncludeXmlComments(xmlPath);
                        opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Description = "Bearer Generated JWT-Token",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"

                        });
                        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = JwtBearerDefaults.AuthenticationScheme
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,
                                }, Array.Empty<string>()
                            }
                        });
                    });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("redis");
            options.InstanceName = "koi_shipment";
        });
        services.AddRouting(x => x.LowercaseUrls = true);
        services.AddSingleton<GlobalErrorHandlingMiddleware>();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("VERYSTRONGPASSWORD_CHANGEMEIFYOUNEED")),
                ValidateIssuer = true,
                ValidIssuer = "ks.api",
                ValidAudience = "ks.client",
                ValidateAudience = true
            };
        });
        services.AddHttpContextAccessor();
        services.AddDbContext<AppDbContext>(opt => opt
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("SqlServer")));
        // DI Auto Registration
        services.Scan(scan =>
        {
            scan.FromAssemblies(getAssemblies())
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime();
        });
        // AutoMapper
        services.AddAutoMapper(typeof(MapperConfigurationProfile));
        services.AddValidatorsFromAssemblies(getAssemblies());


        return services;
    }
    private static Assembly[] getAssemblies()
        => [AssemblyReference.Assembly, application.AssemblyReference.Assembly, infras.AssemblyReference.Assembly];


}