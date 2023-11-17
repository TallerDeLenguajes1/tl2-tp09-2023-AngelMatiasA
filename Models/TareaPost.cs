using System; 
namespace Models; 
public class TareaPost{

    public string? Nombre {get; set;}
    public Estado Estado {get;set;}
    public string? Descripcion {get;set;}
    public string? Color {get; set;}
    public int Id_usuario_asignado {get; set;}

}