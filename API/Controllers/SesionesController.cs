using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SesionesController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public SesionesController(tpi_dabdContext context)
    {
        _context = context;
    }
 
    [HttpGet("CantSesiones")]
    [Authorize]
    public async Task<ActionResult<Reportesesion>> ObtenerVistaReporte()
    {
        var vistaReporte = await _context.Reportesesions.Select(x =>
        new SesionReporteModel
        {
            nombreUsuario = x.Usuario,
            cantidad = x.Cantidad

        }).ToListAsync();
        return Ok(vistaReporte);
    }
}
