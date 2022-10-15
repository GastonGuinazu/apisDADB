using System.IO.Compression;
using System.Data.Common;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<CartasCroupierModel>> Get(int id)
    {
        List<CartasCroupier> cartas = (from c in _context.CartasCroupiers.Where(x => x.IdUsuario == id) select c).ToList();
        if (cartas.Count == 0)
        {
            return NotFound($"No se encontraron cartas con el id {id}");
        }
        foreach (CartasCroupier carta in cartas)
        {
            var cartaCroupierModel = new CartasCroupierModel
            {
                CodCroupier = carta.CodCroupier,
                idCarta = carta.IdCarta,
                idUsuario = carta.IdUsuario
            };

        }
        await _context.SaveChangesAsync();
        return Ok(cartas);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
         List <CartasCroupier> cartas = (from c in _context.CartasCroupiers.Where(x =>x.IdUsuario==id)  select c ).ToList();
         if (cartas.Count == 0)
            {
                return NotFound($"No se encontraron cartas para el usuario con el id {id}");
            }
          foreach(CartasCroupier carta in cartas )
          {
              _context.CartasCroupiers.Remove(carta);
              
          }
        await _context.SaveChangesAsync();
       return Ok();
    }

}
