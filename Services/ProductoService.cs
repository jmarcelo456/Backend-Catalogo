
using ApiInicial.Data;
using ApiInicial.Models;
using Microsoft.EntityFrameworkCore;

public class ProductoService : IProductoService
{
    private readonly AppDbContext _context;
    public ProductoService(AppDbContext context)
    {
        this._context = context;
    }
    public async Task<bool> ActualizarProducto(int id, Producto producto)
    {
        var existe = await _context.Productos.FindAsync(id);
        if (existe is null)
            return false;
        existe.Nombre = producto.Nombre;
        existe.Precio = producto.Precio;
        existe.Stock = producto.Stock;
        existe.Estado = producto.Estado;
        existe.CategoriaId = producto.CategoriaId;
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Producto> CrearProductoAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Productos.FindAsync(id);
        if(existe is null)
            return false;
        _context.Productos.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Producto>> ObtenerTodosAsync()
    {
        return await _context.Productos.ToListAsync();
    }
}