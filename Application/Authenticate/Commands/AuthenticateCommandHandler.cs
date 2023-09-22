using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Exceptions;
using Domain.Models;

namespace Application.Authenticate.Commands;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthenticateCommandHandler(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindByNameAsync(request.Username);
        
        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }

        var token = GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    private string GenerateJwtToken(User user)
    {
        var secureKey = Encoding.UTF8.GetBytes(_configuration["JwtOptions:SigningKey"]);
        var issuer = _configuration["JwtOptions:Issuer"];
        var audience = _configuration["JwtOptions:Audience"];
        var securityKey = new SymmetricSecurityKey(secureKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.Now.AddMinutes(30),
            Audience = audience,
            Issuer = issuer,
            SigningCredentials = credentials,
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}
