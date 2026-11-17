using Microsoft.EntityFrameworkCore;
using Felipe_Ap1_P2.Context;
using Felipe_Ap1_P2.Models;

namespace Felipe_Ap1_P2.Services;

public class Services(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar()
    {
        return await Task.FromResult(true);
    }

    public async Task<bool> Eliminar()
    {
        return await Task.FromResult(true);
    }

    public async Task<bool> Buscar()
    {
        return await Task.FromResult(true);
    }

    public async Task<bool> Crear()
    {
        return await Task.FromResult(true);
    }
}