using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felipe_Ap1_P2.Models;

public class VentasDetalle
{
    [Key]
    public int DetalleId{get; set;}
    public int VentaId{get; set;}

    public int ProductoId{get;set;}

    [ForeignKey("ProductoId")]
    public Productos? Productos{get; set;}

    [Range(1, int.MaxValue, ErrorMessage ="la cantidad debe ser mayor que 0")]
    public int Cantidad{get; set;}

    [Column(TypeName ="decimal(18,2)")]
    public decimal Precio{get;set;}
}