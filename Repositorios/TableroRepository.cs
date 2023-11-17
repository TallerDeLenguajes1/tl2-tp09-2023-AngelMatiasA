using System.Data.SQLite;
using Models;
namespace Repositorios;

public class TableroRepository
{
    private string connectionString = @"Data Source = DB/kanban.db;Initial Catalog=Northwind;Integrated Security=true";

/*
Crear un nuevo tablero (devuelve un objeto Tablero)
● Modificar un tablero existente (recibe un id y un objeto Tablero)
● Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero)
● Listar todos los tableros existentes (devuelve un list de tableros)
● Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un
list de tableros)
● Eliminar un tablero por ID
*/
    public TableroPost CrearTablero(TableroPost nuevoTablero){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            
            string queryString = @"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@Id_usuario_propietario, @Nombre, @Descripcion);";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@Id_usuario_propietario", nuevoTablero.Id_usuario_propietario));
            command.Parameters.Add(new SQLiteParameter("@Nombre", nuevoTablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@Descripcion", nuevoTablero.Descripcion));
           rowAffected = command.ExecuteNonQuery();
            connection.Close();
        }
        if (rowAffected!=1)
        {
            return null ;
        
        }
        return nuevoTablero ;
    }

    public Tablero ModificarTablero(int idTablero, Tablero TableroModificar){
    int rowAffected = 0;
    using(var connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string queryString = @"UPDATE Tablero SET Nombre = @nombreNuevo, Descripcion = @descripcionNueva, Id_usuario_propietario = @idUsuarioNuevo WHERE Id = @idTablero;";
        var command = new SQLiteCommand(queryString, connection);

        command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
        command.Parameters.Add(new SQLiteParameter("@nombreNuevo", TableroModificar.Nombre));
        command.Parameters.Add(new SQLiteParameter("@descripcionNueva", TableroModificar.Descripcion));
        command.Parameters.Add(new SQLiteParameter("@idUsuarioNuevo", TableroModificar.Id_usuario_propietario));
        rowAffected = command.ExecuteNonQuery();
        connection.Close();
    }
    
    if (rowAffected!=1)
    {
        return null ;
    }
    return TableroModificar ;
}

    public List<Tablero> MostrarTableros(){
        List<Tablero> Tableros = new List<Tablero>();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tablero;";
            var command = new SQLiteCommand(queryString, connection);
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var Tablero = new Tablero();
                    Tablero.Id = Convert.ToInt32(reader["id"]);
                    Tablero.Nombre = reader["Nombre"].ToString();
                    Tablero.Descripcion = reader["descripcion"].ToString();
                    Tablero.Id_usuario_propietario=Convert.ToInt32(reader["id_usuario_propietario"]);
                    Tableros.Add(Tablero);
                }
            }
            connection.Close();
        }
        if(Tableros.Count == 0){
            return null;
        }

        return Tableros;
    }

    
       public Tablero MostrarPorId(int idTablero){
        Tablero buscado = new Tablero();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tablero WHERE id = @idTablero";
            
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
            using(var reader = command.ExecuteReader())
            {
                if(reader.Read()){
                    buscado.Id = Convert.ToInt32(reader["id"]);
                    buscado.Nombre = reader["Nombre"].ToString();
                    buscado.Descripcion = reader["descripcion"].ToString();
                    buscado.Id_usuario_propietario=Convert.ToInt32(reader["id_usuario_propietario"]);
                }
            }
            connection.Close();
        }
        if (buscado.Id == 0)
        {
            return null;
        }

        return buscado;
    }

    /*● Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un
list de tableros)*/
public List<Tablero> MostrarPorUsuarioId(int idUsuario){
        List<Tablero> Tableros = new List<Tablero>();
         using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

            using(var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var Tablero = new Tablero();
                    Tablero.Id = Convert.ToInt32(reader["id"]);
                    Tablero.Nombre = reader["Nombre"].ToString();
                    Tablero.Descripcion = reader["descripcion"].ToString();
                    Tablero.Id_usuario_propietario=Convert.ToInt32(reader["id_usuario_propietario"]);
                    Tableros.Add(Tablero);
                }
            }
            connection.Close();
        }
        if(Tableros.Count == 0){
            return null;
        }

        return Tableros;
    }


    public bool EliminarTablero(int idTablero){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"DELETE FROM Tablero WHERE id = @idTablero;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

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