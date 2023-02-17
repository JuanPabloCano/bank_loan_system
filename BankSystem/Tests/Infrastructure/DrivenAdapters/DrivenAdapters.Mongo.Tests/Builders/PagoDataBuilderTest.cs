using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Pagos;

namespace DrivenAdapters.Mongo.Tests.Builders;

public class PagoDataBuilderTest
{
    private static string _id = string.Empty;
    private static string _usuarioId = string.Empty;
    private static string _creditoId = string.Empty;
    private static int _cantidad = 0;
    private static FechaPago FechaPago;

    public PagoDataBuilderTest()
    {
    }

    public PagoDataBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public PagoDataBuilderTest SetUsuarioId(string id)
    {
        _usuarioId = id;
        return this;
    }

    public PagoDataBuilderTest SetCreditoId(string id)
    {
        _creditoId = id;
        return this;
    }

    public PagoDataBuilderTest SetCantidad(int cantidad)
    {
        _cantidad = cantidad;
        return this;
    }

    public PagoDataBuilderTest SetFechaPago(FechaPago fechaPago)
    {
        FechaPago = fechaPago;
        return this;
    }

    public static PagoData Build() => new(_id, _usuarioId, _creditoId, _cantidad, FechaPago);
}