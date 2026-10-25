using System.ComponentModel.DataAnnotations;

public class ProductoRequestDto
{
    [Required]
    [StringLength(2, ErrorMessage = "La propiedad no tiene longitud correcta")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [Range(1, 100, ErrorMessage = "El precio es incorrecto")]
    public decimal Precio { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El stock tiene una proiedad negativa o cero")]
    public int Stock { get; set; }

    public int CategoriaId { get; set; }
}
