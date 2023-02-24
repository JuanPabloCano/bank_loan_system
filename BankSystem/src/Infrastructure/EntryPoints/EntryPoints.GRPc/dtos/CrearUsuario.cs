namespace EntryPoints.GRPc.dtos;

public class CrearUsuario
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Profesion { get; set; }
}