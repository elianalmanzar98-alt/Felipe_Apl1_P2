using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
namespace Felipe_Ap1_P2.Context;

public class Contexto(DbContextOptions <Contexto> options): DbContext(options)
{
    public DbSet<Modelo> Modelos{get; set;}
    
}