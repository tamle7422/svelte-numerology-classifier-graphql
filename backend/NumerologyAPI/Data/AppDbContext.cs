using Microsoft.EntityFrameworkCore;
using NumerologyAPI.Models;

namespace NumerologyAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Prediction> Predictions { get; set; }
}