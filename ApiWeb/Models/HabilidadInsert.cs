using System;
using static ApiWeb.Models.Habilidad;

namespace ApiWeb.Models;

public class HabilidadInsert
{
    public string Nombre { get; set; } = string.Empty;

    public EPotencia Potencia { get; set; }
}
