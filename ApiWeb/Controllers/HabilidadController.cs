using System;
using ApiWeb.Helpers;
using ApiWeb.Models;
using ApiWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers;

[ApiController]
[Route("api/mandril/{mandrilId}/[controller]")]
public class HabilidadController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Habilidad>> GetHabilidades(int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId); 

        if(mandril == null)
        
            return NotFound(Mensajes.Mandril.NotFound);
      
        return Ok(mandril.Habilidades);
    }

    [HttpGet("{habilidadId}")]
    public ActionResult<Habilidad> GetHabilidad(int mandrilId, int habilidadId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId); 

        if(mandril == null)
        
            return NotFound(Mensajes.Mandril.NotFound);
        
        var habilidad = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

        if(habilidad == null)
        
            return NotFound(Mensajes.Habilidad.NotFound);
        
        return Ok(habilidad); 
    }

    [HttpPost]
    public ActionResult<Habilidad> PostHabilidad(int mandrilId, HabilidadInsert habilidadInsert)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId); 

        if(mandril == null)
        
            return NotFound(Mensajes.Mandril.NotFound);
        
        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Nombre == habilidadInsert.Nombre);

        if (habilidadExistente != null)
            return BadRequest(Mensajes.Habilidad.NombreExistente);

        var maxHabilidad = mandril.Habilidades.Max(h => h.Id);

        var habilidadNueva = new Habilidad()
        {
            Id = maxHabilidad + 1,
            Nombre = habilidadInsert.Nombre,
            Potencia = habilidadInsert.Potencia
        };

        mandril.Habilidades.Add(habilidadNueva);

        return CreatedAtAction(nameof(GetHabilidad),
            new{ mandrild = mandrilId, habilidad = habilidadNueva.Id},
            habilidadNueva
        );
    }

    [HttpPut("{habilidadId}")]
    public ActionResult<Habilidad> PutHabilidad(int  mandrilId, int habilidadId, HabilidadInsert habilidadInsert)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId); 

        if(mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);
        
        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

        if (habilidadExistente == null)
            return BadRequest(Mensajes.Habilidad.NotFound);

        var habilidadMismoNombre = mandril.Habilidades?
            .FirstOrDefault(h => h.Id != habilidadId && h.Nombre == habilidadInsert.Nombre);
        
        if(habilidadMismoNombre != null)
            return BadRequest(Mensajes.Habilidad.NombreExistente);

        //Asignacion
        habilidadExistente.Nombre = habilidadInsert.Nombre;
        habilidadExistente.Potencia = habilidadInsert.Potencia;

        return NoContent();
    }

    [HttpDelete("{habilidadId}")]

    public ActionResult<Habilidad> DeleteHabilidad(int mandrilId, int habilidadId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId); 

        if(mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);
        
        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

        if (habilidadExistente == null)
            return BadRequest(Mensajes.Habilidad.NotFound);

        mandril.Habilidades?.Remove(habilidadExistente);

        return NoContent();
    }
}
