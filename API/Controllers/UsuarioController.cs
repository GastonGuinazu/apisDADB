using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public UsuarioController(tpi_dabdContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<UsuarioModel>> Create(UsuarioCreateModel user)
    {
        var newUsuario = new Usuario
        {
            Usuario1 = user.usuario,
            Pass = user.pass

        };
        _context.Usuarios.Add(newUsuario);
        await _context.SaveChangesAsync();

        var usuarioModel = new UsuarioModel
        {
            idUsuario = newUsuario.IdUsuario,
            usuario = newUsuario.Usuario1,
            pass = newUsuario.Pass

        };
        return Ok(usuarioModel);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioModel>> Create(UsuarioModel userModel)
    {

        var user = _context.Usuarios.FirstOrDefault(x => x.Usuario1 == userModel.usuario && x.Pass == userModel.pass);
        if (user == null)
        {
            return Unauthorized("Usuario o Contraseña incorrecta");
        }
        UsuarioModel usuarioRetorno = new UsuarioModel();
        usuarioRetorno.usuario = user.Usuario1;
        // var usuarioModel = new UsuarioModel
        // {
        //     idUsuario = user.idUsuario,
        //     usuario= user.usuario,
        //     pass = user.pass
        // };
        return Ok(usuarioRetorno);
    }

}
