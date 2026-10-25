public interface ICategoriaService
{
    Task<List<CategoriaResponseDto>> ObtenerTodosAsync();
    Task<CategoriaResponseDto> CrearCategoriaAsync(CategoriaRequestDto producto);
    Task<CategoriaResponseDto> ActualizarCategoria(int id, CategoriaRequestDto producto);
    Task<bool> EliminarAsync(int id);
    Task<CategoriaResponseDto> BuscarCategoriaPorIdAsync(int id);
}
