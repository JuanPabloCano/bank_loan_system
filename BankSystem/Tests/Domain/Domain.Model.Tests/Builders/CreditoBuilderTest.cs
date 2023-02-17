using Domain.Model.Entities.Credito;

namespace Domain.Model.Tests.Builders;

public class CreditoBuilderTest
{
    private static string _id = string.Empty;
    private static string _usuarioId = string.Empty;
    private static string _cuentaId = string.Empty;
    private static int _totalPrestamo = 0;
    private static int _deudaActual = 0;

    public CreditoBuilderTest()
    {
    }

    public CreditoBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public CreditoBuilderTest SetUsuarioId(string id)
    {
        _usuarioId = id;
        return this;
    }

    public CreditoBuilderTest SetCuentaId(string id)
    {
        _cuentaId = id;
        return this;
    }

    public CreditoBuilderTest SetTotalPrestamo(int cantidad)
    {
        _totalPrestamo = cantidad;
        return this;
    }

    public CreditoBuilderTest SetDeudaActual(int cantidad)
    {
        _deudaActual = cantidad;
        return this;
    }

    public Credito Build() => new(_id, _usuarioId, _cuentaId, _totalPrestamo, _deudaActual);
}