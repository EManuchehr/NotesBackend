using Application.Common.Helpers;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly INotesDbContext _dbContext;

    public UserService(INotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> FindByNameAsync(string username)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public bool CheckPasswordAsync(User user, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return false;
        }
        
        var passwordHash = Hash.Sha256(user.Password);
        
        return user.Password == passwordHash;
    }
}