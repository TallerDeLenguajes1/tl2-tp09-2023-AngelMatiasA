using System.Data.SQLite;
using Models;
namespace Repositorios;

public class TareaRepository
{
    private string connectionString = @"Data Source = DB/kanban.db;Initial Catalog=Northwind;Integrated Security=true";



    public TareaPost CrearTarea(int idTablero, TareaPost nuevaTarea){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            
            string queryString = @"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@Id_Tablero, @Nombre, @Estado, @Descripcion, @Color, @Id_usuario_asignado);";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@Id_Tablero", idTablero));
            command.Parameters.Add(new SQLiteParameter("@Nombre", nuevaTarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@Estado", nuevaTarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@Descripcion", nuevaTarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@Color", nuevaTarea.Color));
            command.Parameters.Add(new SQLiteParameter("@Id_usuario_asignado", nuevaTarea.Id_usuario_asignado));
           rowAffected = command.ExecuteNonQuery();
            connection.Close();
        }
        if (rowAffected!=1)
        {
            return null ;
        
        }
        return nuevaTarea ;
    }

    public Tarea ModificarTarea(int idTarea, Tarea TareaModificar){
    int rowAffected = 0;
    using(var connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string queryString = @"UPDATE Tarea SET id_tablero = @Id_Tablero, Nombre = @Nombre,"+
        " Estado = @Estado, Descripcion = @Descripcion, Color = @Color, "+
        "id_usuario_asignado = @Id_usuario_asignado WHERE Id = @idTarea;";
        var command = new SQLiteCommand(queryString, connection);
         command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
         command.Parameters.Add(new SQLiteParameter("@Id_Tablero", TareaModificar.Id_Tablero));
            command.Parameters.Add(new SQLiteParameter("@Nombre", TareaModificar.Nombre));
            command.Parameters.Add(new SQLiteParameter("@Estado", TareaModificar.Estado));
            command.Parameters.Add(new SQLiteParameter("@Descripcion", TareaModificar.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@Color", TareaModificar.Color));
            command.Parameters.Add(new SQLiteParameter("@Id_usuario_asignado", TareaModificar.Id_usuario_asignado));

        rowAffected = command.ExecuteNonQuery();
        connection.Close();
    }
    
    if (rowAffected!=1)
    {
        return null ;
    }
    return TareaModificar ;
}

/*● Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
● Modificar una tarea existente. (recibe un id y un objeto Tarea)
● Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
● Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario,
devuelve un list de tareas)
● Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list
de tareas)
● Eliminar una tarea (recibe un IdTarea)
● Asignar Usuario a Tarea (recibe idUsuario y un idTarea)*/
    public List<Tarea> MostrarTareasPorEstado(int estado){
        List<Tarea> Tareas = new List<Tarea>();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tarea WHERE estado = @Estado;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@Estado", estado));
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var Tarea = new Tarea();
                    Tarea.Id = Convert.ToInt32(reader["id"]);
                    Tarea.Id_Tablero = Convert.ToInt32(reader["id_tablero"]);
                    Tarea.Nombre = reader["Nombre"].ToString();
                    Tarea.Estado = (Estado) Convert.ToInt32(reader["estado"]);
                    Tarea.Descripcion = reader["descripcion"].ToString();
                    Tarea.Color = reader["color"].ToString();
                    Tarea.Id_usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                    Tareas.Add(Tarea);
                }
            }
            connection.Close();
        }
        if(Tareas.Count == 0){
            return null;
        }

        return Tareas;
    }
      public List<Tarea> MostrarTareasPorTablero(int idTablero){
        List<Tarea> Tareas = new List<Tarea>();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tarea WHERE id_tablero = @idTablero;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var Tarea = new Tarea();
                    Tarea.Id = Convert.ToInt32(reader["id"]);
                    Tarea.Id_Tablero = Convert.ToInt32(reader["id_tablero"]);
                    Tarea.Nombre = reader["Nombre"].ToString();
                    Tarea.Estado = (Estado) Convert.ToInt32(reader["estado"]);
                    Tarea.Descripcion = reader["descripcion"].ToString();
                    Tarea.Color = reader["color"].ToString();
                    Tarea.Id_usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                    Tareas.Add(Tarea);
                }
            }
            connection.Close();
        }
        if(Tareas.Count == 0){
            return null;
        }

        return Tareas;
    }
     public List<Tarea> MostrarTareasPorUsuario(int idUsuario){
        List<Tarea> Tareas = new List<Tarea>();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsu;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsu", idUsuario));
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var Tarea = new Tarea();
                    Tarea.Id = Convert.ToInt32(reader["id"]);
                    Tarea.Id_Tablero = Convert.ToInt32(reader["id_tablero"]);
                    Tarea.Nombre = reader["Nombre"].ToString();
                    Tarea.Estado = (Estado) Convert.ToInt32(reader["estado"]);
                    Tarea.Descripcion = reader["descripcion"].ToString();
                    Tarea.Color = reader["color"].ToString();
                    Tarea.Id_usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                    Tareas.Add(Tarea);
                }
            }
            connection.Close();
        }
        if(Tareas.Count == 0){
            return null;
        }

        return Tareas;
    }

       public Tarea MostrarPorId(int idTarea){
        Tarea buscada = new Tarea();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tarea WHERE id = @idTarea";
            
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            using(var reader = command.ExecuteReader())
            {
                if(reader.Read()){
                    buscada.Id = Convert.ToInt32(reader["id"]);
                    buscada.Id_Tablero = Convert.ToInt32(reader["id_tablero"]);
                    buscada.Nombre = reader["Nombre"].ToString();
                    buscada.Estado = (Estado) Convert.ToInt32(reader["estado"]);
                    buscada.Descripcion = reader["descripcion"].ToString();
                    buscada.Color = reader["color"].ToString();
                    buscada.Id_usuario_asignado=Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();
        }
        if (buscada.Id == 0)
        {
            return null;
        }

        return buscada;
    }

 public bool AsignarUsuarioaTarea(int idTarea, int idUsu){
    int rowAffected = 0;
    using(var connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string queryString = @"UPDATE Tarea SET id_usuario_asignado = @Id_usuario_asignado WHERE Id = @idTarea;";
        var command = new SQLiteCommand(queryString, connection);
        command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
        command.Parameters.Add(new SQLiteParameter("@Id_usuario_asignado", idUsu));

        rowAffected = command.ExecuteNonQuery();
        connection.Close();
    }
    
    if (rowAffected!=1)
    {
        return false ;
    }
    return true ;
}

 public bool modificarEstado(int idTarea, int Estado){
    int rowAffected = 0;
    using(var connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string queryString = @"UPDATE Tarea SET estado = @Estado WHERE Id = @idTarea;";
        var command = new SQLiteCommand(queryString, connection);
        command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
        command.Parameters.Add(new SQLiteParameter("@Estado", Estado));

        rowAffected = command.ExecuteNonQuery();
        connection.Close();
    }
    
    if (rowAffected!=1)
    {
        return false ;
    }
    return true ;
}
    
    public bool EliminarTarea(int idTarea){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"DELETE FROM Tarea WHERE id = @idTarea;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));

            rowAffected = command.ExecuteNonQuery();
            connection.Close();
        }
         if (rowAffected!=1)
        {
            return false ;
        
        }
        return true;
    }


}