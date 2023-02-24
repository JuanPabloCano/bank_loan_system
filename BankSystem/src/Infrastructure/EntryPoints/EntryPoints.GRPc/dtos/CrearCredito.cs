namespace EntryPoints.GRPc.dtos;

public class CrearCredito
{
    public string UsuarioId { get; set; }
    public string CuentaId { get; set; }
    public int TotalPrestamo { get; set; }
    public int DeudaActual { get; set; }
}