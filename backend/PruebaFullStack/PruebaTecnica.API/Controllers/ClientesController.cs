using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.DTOs.Clientes;
using PruebaTecnica.Application.Interfaces;

namespace PruebaTecnica.API.Controllers;

/// <summary>
/// Controlador CRUD de clientes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    /// <summary>Obtiene todos los clientes.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos()
    {
        var result = await _service.ObtenerTodosAsync();
        return Ok(result.Data);
    }

    /// <summary>Obtiene un cliente por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var result = await _service.ObtenerPorIdAsync(id);
        if (!result.Success) return NotFound(new { mensaje = result.Message });
        return Ok(result.Data);
    }

    /// <summary>Crea un nuevo cliente.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Crear([FromBody] CrearClienteDto dto)
    {
        var result = await _service.CrearAsync(dto);
        if (!result.Success) return BadRequest(new { mensaje = result.Message });
        return CreatedAtAction(nameof(ObtenerPorId), new { id = result.Data!.Id }, result.Data);
    }

    /// <summary>Actualiza un cliente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarClienteDto dto)
    {
        var result = await _service.ActualizarAsync(id, dto);
        if (!result.Success) return NotFound(new { mensaje = result.Message });
        return Ok(result.Data);
    }

    /// <summary>Elimina un cliente.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id)
    {
        var result = await _service.EliminarAsync(id);
        if (!result.Success) return NotFound(new { mensaje = result.Message });
        return NoContent();
    }
}