using Microsoft.EntityFrameworkCore;
 using Felipe_Ap1_P2.Models;
    
    
    
    
    namespace Felipe_Ap1_P2.Context
{


    public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
    {
        public DbSet<Ventas> Ventas { get; set; } 
        public DbSet<VentasDetalle>VentasDetalles{get; set;}
        public DbSet<Productos>Productos{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Productos>().HasData(new List<Productos>()
            {
            new Productos() { ProductoId = 1, Nombre = "Frappuccino Mocha 281 ml",    Precio = 110.00m, Stock = 50 },
            new Productos() { ProductoId = 2, Nombre = "Frappuccino Vainilla 281 ml", Precio = 110.00m, Stock = 50 },
            new Productos() { ProductoId = 3, Nombre = "Frappuccino Caramelo 281 ml", Precio = 115.00m, Stock = 40 },
            new Productos() { ProductoId = 4, Nombre = "Doubleshot Espresso 200 ml",  Precio = 125.00m, Stock = 30 },
            new Productos() { ProductoId = 5, Nombre = "Cold Brew Original 330 ml",   Precio = 135.00m, Stock = 25 },
            new Productos() { ProductoId = 6, Nombre = "Latte Vainilla 220 ml",       Precio = 100.00m, Stock = 60 }
        });

    }
}}