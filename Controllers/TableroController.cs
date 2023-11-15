using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorios;
namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TableroController : ControllerBase
{
    private readonly ILogger<TableroController> _logger;
    private TableroRepository TableroRepo;
    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        TableroRepo = new TableroRepository();
    }

    [HttpGet("/api/Tablero/MostrarTableros")]
    public ActionResult<IEnumerable<Tablero>> MostrarTableros()
    {
        var Tableros = TableroRepo.MostrarTableros();
        if (Tableros.Count == 0)
        {
            return BadRequest("No se pudo obtener la lista de Tableros de la base de datos");

        }
        return Ok(Tableros);
    }

    [HttpGet]
    [Route("/api/Tablero/ObtenerPorId{id}")]
    public ActionResult<Tablero> GetTableroPorId(int id)
    {
        var encontrado = TableroRepo.MostrarPorId(id);
        if (encontrado == null)
        {
            return BadRequest("El Tablero no existe en la base de datos");
        }
        return Ok(encontrado);
    }

    [HttpPost("/api/Tablero/Crear")]
    public ActionResult<string> CrearTablero(TableroPost nuevoTablero)
    {

        if (nuevoTablero != null)
        {
            var Tablero = TableroRepo.CrearTablero(nuevoTablero);
            if (Tablero == null)
            {
                return BadRequest("No se pudo Guardar en la Base de Datos");
            }
            return Ok("Se creo correctamente.");
        }
        else
        {
            return BadRequest("El Tablero recibido no es valido");
        }
    }

    [HttpPut("/api/Tablero/Modificar{id}")]
    public ActionResult<string> ActualizarTablero(int id, Tablero TableroModificar)
    {
        if (TableroModificar != null)
        {

            var modificado = TableroRepo.ModificarTablero(id, TableroModificar);
            if (modificado == null)
            {
                return BadRequest("No se pudo modificar el Tablero en la base de datos");
            }
            return Ok("Se modifico correctamente");
        }
        else
        {
            return BadRequest("Los datos del Tablero ingresado no son validos");
        }
    }
    [HttpDelete("/api/Tablero/Eliminar{id}")]
    public ActionResult<string> EliminarTablero(int id)
    {
        try
        {
            if (!TableroRepo.EliminarTablero(id))
            {
            return BadRequest("Tablero no encontrado");
                
            }
            return Ok("Tablero eliminado ");
        }
        catch (Exception ex)
        {
            return BadRequest(" Error: " + ex.Message);
        }
    }
}