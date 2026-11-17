using Aplicada1.Core;
using Felipe_Ap1_P2.Context;
using Felipe_Ap1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Felipe_Ap1_P2.Services;

public class VentaService(
    IDbContextFactory<Contexto> contextFactory
) : IServiceResult<Ventas, int>
{
    public async Task<Result> Guardar(Ventas venta)
    {
        
        if (venta.Detalles == null || !venta.Detalles.Any())
            return Result.Failure(new Error("Venta Sin Detalle", "Debe agregar al menos un producto a la venta"));

        if (venta.Detalles.Any(d => d.Cantidad <= 0))
            return Result.Failure(new Error("Cantidad inválida", "La cantidad de cada producto debe ser mayor que 0"));

        var errorStock = await ValidarStock(venta);
        if (errorStock != null)
            return Result.Failure(new Error("StockInsuficiente", errorStock));

        var guardado = await Existe(venta.VentaId)
            ? await Modificar(venta)
            : await Insertar(venta);

        return guardado
            ? Result.Success(true)
            : Result.Failure(new Error("Error al guardar", "No se pudo guardar la venta"));
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Ventas.AnyAsync(v => v.VentaId == id);
    }

    private async Task<bool> Insertar(Ventas venta)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        if (!await CalcularTotal(venta, contexto))
            return false;

        contexto.Ventas.Add(venta);
        await AjustarStock(contexto, venta.Detalles, -1);   // la venta RESTA stock

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Ventas venta)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var original = await contexto.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.VentaId == venta.VentaId);

        if (original == null)
            return false;

        if (!await CalcularTotal(venta, contexto))
            return false;

      
        await AjustarStock(contexto, original.Detalles, 1);

        contexto.VentasDetalles.RemoveRange(original.Detalles);
        contexto.VentasDetalles.AddRange(
            venta.Detalles.Select(d => new VentasDetalle
            {
                VentaId = original.VentaId,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                Precio = d.Precio
            }));

        original.Fecha = venta.Fecha;
        original.Observacion = venta.Observacion;
        original.Total = venta.Total;

        
        await AjustarStock(contexto, venta.Detalles, -1);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result<Ventas?>> Buscar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Ventas
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Productos)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VentaId == id);
    }

    public async Task<Result<List<Ventas>>> GetList(Expression<Func<Ventas, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Ventas
            .Include(v => v.Detalles)
            .Where(criterio)
            .OrderByDescending(v => v.VentaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var venta = await contexto.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.VentaId == id);

        if (venta == null)
            return Result.Failure(new Error("Venta no encontrada", "No existe una venta con el identificador indicado"));

        await AjustarStock(contexto, venta.Detalles, 1);    

        contexto.VentasDetalles.RemoveRange(venta.Detalles);
        contexto.Ventas.Remove(venta);

        return await contexto.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure(new Error("No se pudo eliminar", "No se pudo eliminar la venta"));
    }

    
    private async Task<bool> CalcularTotal(Ventas venta, Contexto contexto)
    {
        foreach (var detalle in venta.Detalles)
        {
            if (detalle.DetalleId == 0)
            {
                var producto = await contexto.Productos.FindAsync(detalle.ProductoId);

                if (producto == null)
                    return false;

                detalle.Precio = producto.Precio;
            }
        }

        venta.Total = venta.Detalles.Sum(d => d.Cantidad * d.Precio);
        return true;
    }

   
    private async Task<string?> ValidarStock(Ventas venta)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var cantidadesOriginales = new Dictionary<int, int>();

        if (venta.VentaId != 0)
        {
            cantidadesOriginales = await contexto.VentasDetalles
                .AsNoTracking()
                .Where(d => d.VentaId == venta.VentaId)
                .GroupBy(d => d.ProductoId)
                .Select(g => new { ProductoId = g.Key, Cantidad = g.Sum(d => d.Cantidad) })
                .ToDictionaryAsync(x => x.ProductoId, x => x.Cantidad);
        }

        foreach (var grupo in venta.Detalles.GroupBy(d => d.ProductoId))
        {
            var producto = await contexto.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductoId == grupo.Key);

            if (producto == null)
                return "Uno de los productos no existe";

            var disponible = producto.Stock +
                (cantidadesOriginales.TryGetValue(grupo.Key, out var original) ? original : 0);

            if (grupo.Sum(d => d.Cantidad) > disponible)
                return $"Stock insuficiente de {producto.Nombre}. Disponible: {disponible}";
        }

        return null;
    }

    
    private async Task AjustarStock(Contexto contexto, IEnumerable<VentasDetalle> detalles, int signo)
    {
        foreach (var grupo in detalles.GroupBy(d => d.ProductoId))
        {
            var producto = await contexto.Productos.FirstAsync(p => p.ProductoId == grupo.Key);
            producto.Stock += signo * grupo.Sum(d => d.Cantidad);
        }
    }

    public async Task<List<Productos>> ListarProductos()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Productos.AsNoTracking().ToListAsync();
    }
}