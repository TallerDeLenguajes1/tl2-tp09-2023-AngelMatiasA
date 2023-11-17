using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorios;
namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase
{
    private readonly ILogger<TareaController> _logger;
    private TareaRepository TareaRepo;
    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        TareaRepo = new TareaRepository();
    }

    // [HttpGet("/api/Tarea/MostrarTareas")]
    // public ActionResult<IEnumerable<Tarea>> MostrarTareas()
    // {
    //     var Tareas = TareaRepo.MostrarTareas();
    //     if (Tareas.Count == 0)
    //     {
    //         return BadRequest("No se pudo obtener la lista de Tareas de la base de datos");

    //     }
    //     return Ok(Tareas);
    // }

     [HttpGet]
    [Route("/api/Tarea/ObtenerPorUsuarioId")]
    public ActionResult<Tarea> GetTareaPorUsuarioId(int idUsuario)
    {
        var encontrado = TareaRepo.MostrarTareasPorUsuario(idUsuario);
        if (encontrado == null)
        {
            return BadRequest("El usuario no tiene Tareas almacenados en la base de datos");
        }
        return Ok(encontrado);
    }

         [HttpGet]
    [Route("/api/Tarea/ObtenerPorTableroId")]
    public ActionResult<Tarea> GetTareaPorTablerooId(int idTablero)
    {
        var encontrado = TareaRepo.MostrarTareasPorTablero(idTablero);
        if (encontrado == null)
        {
            return BadRequest("El usuario no tiene Tareas almacenados en la base de datos");
        }
        return Ok(encontrado);
    }
          [HttpGet]
    [Route("/api/Tarea/ObtenerPorEstado")]
    public ActionResult<Tarea> GetTareaPorEstado(int Estado)
    {
        var encontrado = TareaRepo.MostrarTareasPorEstado(Estado);
        if (encontrado == null)
        {
            return BadRequest("no hay tareas con ese estado");
        }
        return Ok(encontrado);
    }

    [HttpGet]
    [Route("/api/Tarea/ObtenerPorId{id}")]
    public ActionResult<Tarea> GetTareaPorId(int id)
    {
        var encontrado = TareaRepo.MostrarPorId(id);
        if (encontrado == null)
        {
            return BadRequest("El Tarea no existe en la base de datos");
        }
        return Ok(encontrado);
    }


    [HttpPost("/api/Tarea/Crear")]
    public ActionResult<TareaPost> CrearTarea(int idTablero, TareaPost nuevaTarea)
    {

        if (nuevaTarea != null)
        {
            var Tarea = TareaRepo.CrearTarea(idTablero, nuevaTarea);
            if (Tarea == null)
            {
                return BadRequest("No se pudo Guardar en la Base de Datos");
            }
            return Ok(Tarea);
        }
        else
        {
            return BadRequest("El Tarea recibido no es valido");
        }
    }

    [HttpPut("/api/Tarea/AsignarAUsuario")]
    public ActionResult<string> ActualizarTarea(int idTarea, int idUsu)
    {
        
        var modificado = TareaRepo.AsignarUsuarioaTarea(idTarea, idUsu);
        if (!modificado )
        {
            return BadRequest("No se pudo modificar el Tarea en la base de datos");
        }
        return Ok("Se modifico correctamente");
        
    }

     [HttpPut("/api/Tarea/CambiarEstado")]
    public ActionResult<bool> ModificarEstado(int idTarea, int estado)
    {
        
        var modificado = TareaRepo.modificarEstado(idTarea, estado);
        if (!modificado )
        {
            return BadRequest("No se pudo modificar el Tarea en la base de datos");
        }
        return Ok(modificado);
        
    }

      [HttpPut("/api/Tarea/Modificar")]
    public ActionResult<Tarea> ActualizarTarea(int id, Tarea TareaModificar)
    {
        if (TareaModificar != null)
        {

            var modificado = TareaRepo.ModificarTarea(id, TareaModificar);
            if (modificado == null)
            {
                return BadRequest("No se pudo modificar el Tarea en la base de datos");
            }
            return Ok(TareaModificar);
        }
        else
        {
            return BadRequest("Los datos del Tarea ingresado no son validos");
        }
    }


    [HttpDelete("/api/Tarea/Eliminar{id}")]
    public ActionResult<string> EliminarTarea(int id)
    {
        try
        {
            if (!TareaRepo.EliminarTarea(id))
            {
            return BadRequest("Tarea no encontrado");
                
            }
            return Ok("Tarea eliminado ");
        }
        catch (Exception ex)
        {
            return BadRequest(" Error: " + ex.Message);
        }
    }
}