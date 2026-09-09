using EnterpriseDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDashboard.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Metric> Metrics => Set<Metric>();
}
