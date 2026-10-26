using System.ComponentModel;
using ApiInicial.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaResponseDto>>> ObtenerTodos()
    {
        return Ok(await _categoriaService.ObtenerTodosAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaResponseDto>> ObtenerPorId(int id)
    {
        var categoria = await _categoriaService.BuscarCategoriaPorIdAsync(id);
        return categoria is null ? NotFound() : Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> CrearCategoria(
        CategoriaRequestDto request
    )
    {
        var creada = await _categoriaService.CrearCategoriaAsync(request);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CategoriaResponseDto>> ActualizarCategoria(
        int id,
        CategoriaRequestDto request
    )
    {
        var actualizada = await _categoriaService.ActualizarCategoria(id, request);
        return actualizada is null ? NotFound() : Ok(actualizada);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        var eliminada = await _categoriaService.EliminarAsync(id);
        return eliminada ? NoContent() : NotFound();
    }
}
