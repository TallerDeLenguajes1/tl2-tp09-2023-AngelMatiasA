using System; 
namespace Models; 
public class Tarea{

    public int Id {get; set;}
    public int Id_tablero {get; set;}
    public string? Nombre {get; set;}
    public Estado Estado {get;set;}
    public string? Descripcion {get;set;}
    public string? Color {get; set;}
    public int Id_usuario_asignado {get; set;}

}

public enum Estado{
    ToDo, 
    Doing, 
    Review, 
    Done
}