using Microsoft.EntityFrameworkCore;
using Openings.Models;

namespace Openings.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

    public DbSet<Opening> Openings { get; set; }
}