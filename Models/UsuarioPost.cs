using System;
using System.Text.Json.Serialization;

namespace Models; 
public class UsuarioPost{
    [JsonIgnore]
    public int Id {get; set;}
    public string? Nombre_de_usuario {get; set;}

}