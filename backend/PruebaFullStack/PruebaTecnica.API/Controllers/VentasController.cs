using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.DTOs.Ventas;
using PruebaTecnica.Application.Interfaces;

namespace PruebaTecnica.API.Controllers;

/// <summary>
/// Controlador para registrar y consultar ventas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class VentasController : ControllerBase
{
    private readonly IVentaService _service;

    public VentasController(IVentaService service)
    {
        _service = service;
    }

    /// <summary>Obtiene todas las ventas con sus detalles y cliente.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodas()
    {
        var result = await _service.ObtenerTodasAsync();
        return Ok(result.Data);
    }

    /// <summary>Obtiene una venta por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var result = await _service.ObtenerPorIdAsync(id);
        if (!result.Success) return NotFound(new { mensaje = result.Message });
        return Ok(result.Data);
    }

    /// <summary>Registra una nueva venta descontando stock automáticamente.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearVentaDto dto)
    {
        var result = await _service.CrearAsync(dto);
        if (!result.Success) return BadRequest(new { mensaje = result.Message });
        return StatusCode(StatusCodes.Status201Created, result.Data);
    }
}