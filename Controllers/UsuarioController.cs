using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorios;
namespace Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private UsuarioRepository usuarioRepo;
    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepo = new UsuarioRepository();
    }

    [HttpGet("/api/usuario/MostrarUsuarios")]
    public ActionResult<IEnumerable<Usuario>> MostrarUsuarios()
    {
        var usuarios = usuarioRepo.MostrarUsuarios();
        if (usuarios.Count == 0)
        {
            return BadRequest("No se pudo obtener la lista de usuarios de la base de datos");

        }
        return Ok(usuarios);
    }

    [HttpGet]
    [Route("/api/usuario/ObtenerPorId{id}")]
    public ActionResult<Usuario> GetUsuarioPorId(int id)
    {
        var encontrado = usuarioRepo.MostrarPorId(id);
        if (encontrado == null)
        {
            return BadRequest("El usuario no existe en la base de datos");
        }
        return Ok(encontrado);
    }

    [HttpPost("/api/usuario/Crear")]
    public ActionResult<string> CrearUsuario(UsuarioPost nuevoUsuario)
    {

        if (nuevoUsuario != null)
        {
            var usuario = usuarioRepo.CrearUsuario(nuevoUsuario);
            if (usuario == null)
            {
                return BadRequest("No se pudo Guardar en la Base de Datos");
            }
            return Ok("Se creo correctamente.");
        }
        else
        {
            return BadRequest("El usuario recibido no es valido");
        }
    }

    [HttpPut("/api/usuario/Modificar{id}")]
    public ActionResult<string> ActualizarUsuario(int id, UsuarioPost usuarioModificar)
    {
        if (usuarioModificar != null)
        {

            var modificado = usuarioRepo.ModificarUsuario(id, usuarioModificar);
            if (modificado == null)
            {
                return BadRequest("No se pudo modificar el usuario en la base de datos");
            }
            return Ok("Se modifico correctamente");
        }
        else
        {
            return BadRequest("Los datos del Usuario ingresado no son validos");
        }
    }
    [HttpDelete("/api/usuario/Eliminar{id}")]
    public ActionResult<string> EliminarUsuario(int id)
    {
        try
        {
            if (!usuarioRepo.EliminarUsuario(id))
            {
            return BadRequest("Usuario no encontrado");
                
            }
            return Ok("Usuario eliminado ");
        }
        catch (Exception ex)
        {
            return BadRequest(" Error: " + ex.Message);
        }
    }
}