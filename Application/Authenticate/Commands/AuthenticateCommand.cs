using MediatR;

namespace Application.Authenticate.Commands;

public class AuthenticateCommand : IRequest<AuthenticateResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}