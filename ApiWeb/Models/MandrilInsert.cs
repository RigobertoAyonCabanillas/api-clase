using System;
using System.ComponentModel.DataAnnotations;

namespace ApiWeb.Models;

public class MandrilInsert
{
    [Required]
    [MaxLength(10)]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    [MaxLength(10)]
    public string Apellido { get; set; } = string.Empty;
}
