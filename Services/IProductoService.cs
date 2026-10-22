
using ApiInicial.Models;

public interface IProductoService
{
    Task<List<Producto>> ObtenerTodosAsync();
    Task<Producto> CrearProductoAsync(Producto producto);
    Task<bool> ActualizarProducto(int id, Producto producto);
    Task<bool> EliminarAsync(int id);
}