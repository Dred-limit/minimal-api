using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Data;

public class FruitDbContext : DbContext
{

    public FruitDbContext(DbContextOptions<FruitDbContext> options)
        : base(options) 
        { 
        }


    public DbSet<Fruit> Fruits { get; set; }
}
