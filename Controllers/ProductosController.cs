using System.ComponentModel;
using ApiInicial.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/productos")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<List<ProductoResponseDto>> ObtenerTodos()
    {
        return await _productoService.ObtenerTodosAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> EliminarProducto(int id)
    {
        return await _productoService.EliminarAsync(id);
    }

    [HttpPost]
    public async Task<ProductoResponseDto> CrearProducto(ProductoRequestDto producto)
    {
        return await _productoService.CrearProductoAsync(producto);
    }

    [HttpPatch("{id:int}")] //Por URL
    public async Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto)
    {
        return await _productoService.ActualizarProducto(id, producto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoResponseDto>> ObtenerPorId(int id)
    {
        var producto = await _productoService.BuscarProductoPorIdAsync(id);
        return producto is null ? NotFound() : Ok(producto);
    }
}
