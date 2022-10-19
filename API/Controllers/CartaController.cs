using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Authorization;

using API.Data;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartaController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartaController(tpi_dabdContext context)
    {
        _context = context;
    }
    
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Carta>> Get()
    {
        var mazo = await _context.Cartas.Select(x => 
        new CartaModel
        {
            id = x.Id,
            carta = x.Carta1,
            valor = x.Valor
        }).ToListAsync();
        return Ok(mazo);
    }
    
}