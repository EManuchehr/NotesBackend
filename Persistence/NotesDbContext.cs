using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain;
using Domain.Models;
using Persistence.EntityTypeConfigurations;

namespace Persistence;

public class NotesDbContext : DbContext, INotesDbContext 
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
    
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new NoteConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(builder);
    }
}