using System;

namespace Domain.Models;

public class Note
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Details { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
} 