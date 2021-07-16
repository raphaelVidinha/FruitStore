using Microsoft.EntityFrameworkCore;
using FruitStore.Models;

namespace FruitStore.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }

        public DbSet<Fruit> Fruits { get; set; }

    }
}
