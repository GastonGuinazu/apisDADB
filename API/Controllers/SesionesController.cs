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

 //   [HttpPost("CrearSesion")]
  //  [Authorize]
    // public async Task<ActionResult<SesionModel>> CrearSesion(SesionesCreateModel sesion)
    // {
    //     var newSesion = new Sesione
    //     {

    //         IdUsuario = sesion.idUsuario

    //     };
    //     _context.Sesiones.Add(newSesion);
    //     await _context.SaveChangesAsync();

    //     var sesionModel = new SesionModel
    //     {

    //         idSesion = newSesion.IdSesion,
    //         idUsuario = newSesion.IdUsuario

    //     };
    //     return Ok(sesionModel);
    // }

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
