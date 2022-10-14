using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartasSinJugarController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartasSinJugarController(tpi_dabdContext context)
    {
        _context = context;
    }

     [HttpPost]
    public async Task<ActionResult<CartasSinJugarModel>> Create(CartasSinJugarCreateModel carta)
    {
        var newCartasSinJugar = new CartasSinJugar
        {
            IdCarta = carta.IdCarta,
            IdUsuario = carta.IdUsuario

        };
        _context.CartasSinJugars.Add(newCartasSinJugar);
        await _context.SaveChangesAsync();

        var cartaSinJugarModel = new CartasSinJugarModel
        {   
            CodSinJugar = newCartasSinJugar.CodSinJugar,         
            IdCarta = newCartasSinJugar.IdCarta,
            IdUsuario = newCartasSinJugar.IdUsuario

        };
        return Ok(cartaSinJugarModel);
    }


    [HttpGet]
    public async Task<ActionResult<CartasSinJugar>> Get()
    {
        var cartasSinJugar = await _context.CartasSinJugars.Select(x => 
        new CartasSinJugarModel
        {
            CodSinJugar = x.CodSinJugar,
            IdCarta = x.IdCarta,
            IdUsuario = x.IdUsuario
            
        }).ToListAsync();
        return Ok(cartasSinJugar);
    }

}
