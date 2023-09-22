using System.Reflection;
using System.Text;
using Application.Common.Behaviors;
using Application.Services;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped<IUserService, UserService>();
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq();
        });

        services.AddMassTransitHostedService();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "manu1411@mail.ru", 
                    ValidAudience = "manu1411@mail.ru", 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0132b23d48e45ade97a8cda58aea57d99642a1b972b1bd7ac662cb2de927967c")),
                };
            });

        services.AddAuthentication();
        services.AddAuthorization();
        
        return services;
    }
}