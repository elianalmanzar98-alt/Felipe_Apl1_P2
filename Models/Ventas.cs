using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felipe_Ap1_P2.Models;

public class Ventas{
     [Key]
    public int VentaId{ get; set; }

    [Required(ErrorMessage ="La fecha es obligatoria")]
    public DateTime Fecha{get; set;}=DateTime.Today;

    [StringLength(200,ErrorMessage ="200 caracteres maximo")]
    public string Observacion{get; set;}= string.Empty;
    [Column(TypeName ="decimal(18,2)")]
    public decimal Total{get; set;}

    [Column(TypeName ="decimal(18,2)")]
    [ForeignKey("ventaId")]
    public ICollection<VentasDetalle>Detalles {get; set;}=new List<VentasDetalle>();
}

  


