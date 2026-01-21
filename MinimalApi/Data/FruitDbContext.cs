using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Data;

public class FruitDbContext : DbContext
{

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=fruit.db;");
    }


    public DbSet<Fruit> Fruits { get; set; }
}
