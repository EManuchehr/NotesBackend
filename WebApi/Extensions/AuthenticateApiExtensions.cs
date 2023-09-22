using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Authenticate.Commands;
using WebApi.Models;

namespace WebApi.Extensions;

public static class AuthenticateApiExtensions
{
    public static WebApplication MapAuthenticateApi(this WebApplication app)
    {
        app.MapPost("/api/auth/login", [AllowAnonymous] async (IMediator mediator, IMapper mapper, [FromForm] LoginDto loginDto) =>
        {
            var command = mapper.Map<AuthenticateCommand>(loginDto);
            var response = await mediator.Send(command);

            return Results.Ok(response);
        });

        return app;
    }
}