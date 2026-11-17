using Microsoft.EntityFrameworkCore;
 using Felipe_Ap1_P2.Models;
    
    
    
    
    namespace Felipe_Ap1_P2.Context
{


    public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
    {
        public DbSet<Modelo> Modelos { get; set; } 
    }
}