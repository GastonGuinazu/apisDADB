using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartasJugadasController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartasJugadasController(tpi_dabdContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<CartasJugadasModel>> Create(CartasJugadasCreateModel carta)
    {
        var newCartaJugada = new CartasJugada
        {
            IdCarta = carta.idCarta,
            IdUsuario = carta.idUsuario

        };
        _context.CartasJugadas.Add(newCartaJugada);
        await _context.SaveChangesAsync();

        var cartaJugadaModel = new CartasJugadasModel
        {   
            codJugadas = newCartaJugada.CodJugadas,         
            idCarta = newCartaJugada.IdCarta,
            idUsuario = newCartaJugada.IdUsuario

        };
        return Ok(cartaJugadaModel);
    }


    [HttpGet]
    public async Task<ActionResult<CartasJugada>> Get()
    {
        var cartasJugadas = await _context.CartasJugadas.Select(x => 
        new CartasJugadasModel
        {
            codJugadas = x.CodJugadas,
            idCarta = x.IdCarta,
            idUsuario = x.IdUsuario
            
        }).ToListAsync();
        return Ok(cartasJugadas);
    }
}
