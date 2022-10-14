using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartasCroupierController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartasCroupierController(tpi_dabdContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<ActionResult<CartasCroupierModel>> Create(CartasCroupierCreateModel carta)
    {
        var newCartaCroupier = new CartasCroupier
        {
            IdCarta = carta.idCarta,
            IdUsuario = carta.idUsuario

        };
        _context.CartasCroupiers.Add(newCartaCroupier);
        await _context.SaveChangesAsync();

        var cartaCroupierModel = new CartasCroupierModel
        {   
            CodCroupier = newCartaCroupier.CodCroupier,         
            idCarta = newCartaCroupier.IdCarta,
            idUsuario = newCartaCroupier.IdUsuario

        };
        return Ok(cartaCroupierModel);
    }


    [HttpGet]
    public async Task<ActionResult<CartasCroupier>> Get()
    {
        var cartasCroupier = await _context.CartasCroupiers.Select(x => 
        new CartasCroupierModel
        {
            CodCroupier = x.CodCroupier,
            idCarta = x.IdCarta,
            idUsuario = x.IdUsuario
            
        }).ToListAsync();
        return Ok(cartasCroupier);
    }


    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(int id)
    // {
    //     var usuario = await _context.CartasCroupiers.SingleOrDefaultAsync(x => x.IdUsuario == id);
    //     if(usuario == null)
    //     {
    //         return NotFound($"No se encontraron las cartas del usuario {id}");
    //     }

    //     _context.CartasCroupiers.Remove(usuario);
    //     await _context.SaveChangesAsync();
        
    //     return Ok();
    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(int id)
    // {
    //     var user = await _context.CartasCroupiers.Where(x => x.IdUsuario == id);
    //     if (user == null)
    //     {
    //         return NotFound($"No se encontró el User con el id {id}");
    //     }

    //     _context.CartasCroupiers.Remove(user);
    //     await _context.SaveChangesAsync();

    //     return Ok();
    // }

    // [HttpDelete("{idUsuario}")]
    // public async Task<ActionResult> Delete(int idUsuario)
    // {
    //     var cartas = await _context.CartasCroupiers
    //     .Where(x => x.IdUsuario.Equals(idUsuario))
    //     .ToListAsync();

    //     if(idUsuario == 0)
    //     {
    //         return NotFound($"No se encontró el User con el id {idUsuario}");
    //     }

    //     _context.CartasCroupiers.Remove(cartas);

    //     return Ok();
    // }
}
