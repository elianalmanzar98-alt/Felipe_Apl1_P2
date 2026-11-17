using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felipe_Ap1_P2.Models;

public class Productos
{
    [Key]
    public int ProductoId{get; set;}
    [Required(ErrorMessage ="El nombre es obligatorio")]
    public string Nombre{get; set;}=string.Empty;
    [Column(TypeName ="decimal(18,2)")]
    public decimal Precio{get; set;}
    public int Stock{get; set;}
}