public interface ICategoriaService
{
    Task<List<CategoriaResponseDto>> ObtenerTodosAsync();
    Task<CategoriaResponseDto?> BuscarCategoriaPorIdAsync(int id);
    Task<CategoriaResponseDto> CrearCategoriaAsync(CategoriaRequestDto categoria);
    Task<CategoriaResponseDto?> ActualizarCategoria(int id, CategoriaRequestDto categoria);
    Task<bool> EliminarAsync(int id);
}
