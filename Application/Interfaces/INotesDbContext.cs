using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Application.Interfaces;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; set; }
    DbSet<User> Users { get; set; }
        
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}