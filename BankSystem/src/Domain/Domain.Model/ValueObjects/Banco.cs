namespace Domain.Model.ValueObjects;

/// <summary>
/// Objeto de valor Banco
/// </summary>
public class Banco
{
    /// <summary>
    /// Nombre
    /// </summary>
    public string Nombre { get; set; }
    
    /// <summary>
    /// Ciudad
    /// </summary>
    public string Ciudad { get; set; }
    
    /// <summary>
    /// Direccion
    /// </summary>
    public string Direccion { get; set; }

    /// <summary>
    /// Crea una instancia de la clase Banco con todos los atributos
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="ciudad"></param>
    /// <param name="direccion"></param>
    public Banco(string nombre, string ciudad, string direccion)
    {
        Nombre = nombre;
        Ciudad = ciudad;
        Direccion = direccion;
    }
}