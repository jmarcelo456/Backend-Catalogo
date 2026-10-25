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

    public async Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto)
    {
        var existe = await _context
            .Productos.Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (existe is null)
            return null;
        existe.Nombre = producto.Nombre;
        existe.Precio = producto.Precio;
        existe.Stock = producto.Stock;
        //existe.Estado = producto.Estado;
        existe.CategoriaId = producto.CategoriaId;

        await _context.SaveChangesAsync();

        return new ProductoResponseDto()
        {
            Id = existe.Id,
            Nombre = existe.Nombre,
            Precio = existe.Precio,
            Stock = existe.Stock,
            CategoriaId = existe.CategoriaId,
            NombreCategoria = existe.Categoria.Descripcion,
        };
    }

    public async Task<ProductoResponseDto> BuscarProductoPorIdAsync(int id)
    {
        var existe = await _context.Productos.Include(p => p.Categoria).FirstAsync(p => p.Id == id);
        if (existe is null)
            return null;
        return new ProductoResponseDto()
        {
            Id = existe.Id,
            Nombre = existe.Nombre,
            Precio = existe.Precio,
            Stock = existe.Stock,
            Estado = existe.Estado,
            CategoriaId = existe.CategoriaId,
            NombreCategoria = existe.Categoria.Descripcion,
        };
    }

    public async Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto productoRequest)
    {
        var prod = new Producto()
        {
            Nombre = productoRequest.Nombre,
            Precio = productoRequest.Precio,
            Stock = productoRequest.Stock,
            Estado = true,
            CategoriaId = productoRequest.CategoriaId,
        };
        _context.Productos.Add(prod);
        await _context.SaveChangesAsync();
        return await this.BuscarProductoPorIdAsync(prod.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Productos.FindAsync(id);
        if (existe is null)
            return false;
        _context.Productos.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ProductoResponseDto>> ObtenerTodosAsync()
    {
        return await _context
            .Productos.Include(p => p.Categoria)
            .Select(p => new ProductoResponseDto()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Estado = p.Estado,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                NombreCategoria = p.Categoria.Descripcion,
            })
            .ToListAsync();
    }
}
