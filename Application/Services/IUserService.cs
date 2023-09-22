using Domain.Models;

namespace Application.Services;

public interface IUserService
{
    public Task<User?> FindByNameAsync(string username);
    public bool CheckPasswordAsync(User user, string password);
}