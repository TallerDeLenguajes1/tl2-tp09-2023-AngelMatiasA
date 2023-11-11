using System.Data.SQLite;
using Models;
namespace Repositorios;

public class UsuarioRepository
{
    private string connectionString = @"Data Source = DB/kanban.db;Initial Catalog=Northwind;Integrated Security=true";

/*
● Crear un nuevo usuario. (recibe un objeto Usuario)
● Modificar un usuario existente. (recibe un Id y un objeto Usuario)
● Listar todos los usuarios registrados. (devuelve un List de Usuarios)
● Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)
● Eliminar un usuario por ID
*/
    public UsuarioPost CrearUsuario(UsuarioPost nuevoUsuario){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            
            string queryString = @"INSERT INTO Usuario (nombre_de_usuario) VALUES (@Nombre_de_usuario);";
            var command = new SQLiteCommand(queryString, connection);

            command.Parameters.Add(new SQLiteParameter("@Nombre_de_usuario", nuevoUsuario.Nombre_de_usuario));
           rowAffected = command.ExecuteNonQuery();
            connection.Close();
        }
        if (rowAffected!=1)
        {
            return null ;
        
        }
        return nuevoUsuario ;
    }

    public UsuarioPost ModificarUsuario(int idUsuario, UsuarioPost usuarioModificar){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string queryString = @"UPDATE Usuario SET nombre_de_usuario = @nombreNuevo WHERE id = @idUsuario;";
            var command = new SQLiteCommand(queryString, connection);

            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@nombreNuevo", usuarioModificar.Nombre_de_usuario));
           rowAffected = command.ExecuteNonQuery();
            connection.Close();
        }
          if (rowAffected!=1)
        {
            return null ;
        
        }
        return usuarioModificar ;
    }

    public List<Usuario> MostrarUsuarios(){
        List<Usuario> usuarios = new List<Usuario>();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Usuario;";
            var command = new SQLiteCommand(queryString, connection);
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }
        if(usuarios.Count == 0){
            return null;
        }

        return usuarios;
    }

    public Usuario MostrarPorId(int idUsuario){
        Usuario buscado = new Usuario();
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Usuario WHERE id = @idUsuario";
            
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            using(var reader = command.ExecuteReader())
            {
                if(reader.Read()){
                    buscado.Id = Convert.ToInt32(reader["id"]);
                    buscado.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
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

    public bool EliminarUsuario(int idUsuario){
        int rowAffected = 0;
        using(var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"DELETE FROM Usuario WHERE id = @idUsuario;";
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

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