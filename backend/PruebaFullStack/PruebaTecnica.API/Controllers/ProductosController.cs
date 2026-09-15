using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.DTOs.Productos;
using PruebaTecnica.Application.Interfaces;

namespace PruebaTecnica.API.Controllers;

/// <summary>
/// Controlador CRUD de productos.
/// Requiere autenticación JWT en todos los endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    /// <summary>Obtiene la lista completa de productos.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos()
    {
        var result = await _service.ObtenerTodosAsync();
        return Ok(result.Data);
    }

    /// <summary>Obtiene un producto por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var result = await _service.ObtenerPorIdAsync(id);
        if (!result.Success) return NotFound(new { mensaje = result.Message });
        return Ok(result.Data);
    }

    /// <summary>Crea un nuevo producto.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearProductoDto dto)
    {
        var result = await _service.CrearAsync(dto);
        if (!result.Success) return BadRequest(new { mensaje = result.Message });
        return CreatedAtAction(nameof(ObtenerPorId), new { id = result.Data!.Id }, result.Data);
    }

    /// <summary>Actualiza un producto existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
    {
        var result = await _service.ActualizarAsync(id, dto);
        if (!result.Success) return NotFound(new { mensaje = result.Message });
        return Ok(result.Data);
    }

    /// <summary>Elimina un producto por su id.</summary>
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