using DrivenAdapters.Mongo.Entities.Creditos;

namespace DrivenAdapters.Mongo.Tests.Builders;

public class CreditoDataBuilderTest
{
    private static string _id = string.Empty;
    private static string _usuarioId = string.Empty;
    private static string _cuentaId = string.Empty;
    private static int _totalPrestamo = 0;
    private static int _deudaActual = 0;

    public CreditoDataBuilderTest()
    {
    }

    public CreditoDataBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public CreditoDataBuilderTest SetUsuarioId(string id)
    {
        _usuarioId = id;
        return this;
    }

    public CreditoDataBuilderTest SetCuentaId(string id)
    {
        _cuentaId = id;
        return this;
    }

    public CreditoDataBuilderTest SetTotalPrestamo(int cantidad)
    {
        _totalPrestamo = cantidad;
        return this;
    }

    public CreditoDataBuilderTest SetDeudaActual(int cantidad)
    {
        _deudaActual = cantidad;
        return this;
    }

    public static CreditoData Build() => new(_id, _usuarioId, _cuentaId, _totalPrestamo, _deudaActual);

}