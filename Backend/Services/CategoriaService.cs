using ApiInicial.Data;
using ApiInicial.Models;
using Microsoft.EntityFrameworkCore;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaResponseDto>> ObtenerTodosAsync()
    {
        return await _context
            .Categorias.AsNoTracking()
            .Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                CantidadProductos = c.Productos!.Count,
            })
            .ToListAsync();
    }

    public async Task<CategoriaResponseDto?> BuscarCategoriaPorIdAsync(int id)
    {
        return await _context
            .Categorias.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                CantidadProductos = c.Productos!.Count,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CategoriaResponseDto> CrearCategoriaAsync(CategoriaRequestDto request)
    {
        var categoria = new Categoria
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CantidadProductos = 0,
        };
    }

    public async Task<CategoriaResponseDto?> ActualizarCategoria(
        int id,
        CategoriaRequestDto request
    )
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null)
            return null;

        categoria.Nombre = request.Nombre;
        categoria.Descripcion = request.Descripcion;
        await _context.SaveChangesAsync();

        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CantidadProductos = await _context.Productos.CountAsync(p => p.CategoriaId == id),
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null)
            return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}
