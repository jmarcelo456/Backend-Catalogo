
using System.ComponentModel;
using ApiInicial.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/productos")]
public class ProductosController : ControllerBase
{
    private ProductoService _productoService;

    public ProductosController(ProductoService productoService)
    {
        this._productoService = productoService;
    }

    [HttpGet]
    public async Task<List<Producto>> ObtenerTodos()
    {
        return await _productoService.ObtenerTodosAsync();
    }

    [HttpDelete("id:int")]
    public async Task<bool> EliminarProducto(int id)
    {
        return await _productoService.EliminarAsync(id);
    }
}