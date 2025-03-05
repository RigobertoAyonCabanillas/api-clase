using System;

namespace ApiWeb.Helpers;

public static class Mensajes
{
    public static class Mandril 
    {
        public const string NotFound = "El mandril solicitado no existe";
    }

    public static class Habilidad
    {
        public const string NotFound = "La habilidad no existe";

        public const string NombreExistente = "Ya existe una habilidad con ese nombre";
    }
}
