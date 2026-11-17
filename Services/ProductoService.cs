





using System.Linq.Expressions;
using Aplicada1.Core;
using Felipe_Ap1_P2.Context;
using Felipe_Ap1_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace Felipe_Ap1_P2.Services;

public class ProductoService(IDbContextFactory<Contexto> contextFactory)
{
    public async Task<Result<List<Productos>>> GetList(Expression<Func<Productos, bool>> Criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(Criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
