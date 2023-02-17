using Domain.Model.Entities.Pago;
using Domain.Model.ValueObjects;

namespace Domain.Model.Tests.Builders;

public class PagoBuilderTest
{
    private static string _id = string.Empty;
    private static string _usuarioId = string.Empty;
    private static string _creditoId = string.Empty;
    private static int _cantidad = 0;
    private static FechaPago FechaPago;

    public PagoBuilderTest()
    {
    }

    public PagoBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public PagoBuilderTest SetUsuarioId(string id)
    {
        _usuarioId = id;
        return this;
    }

    public PagoBuilderTest SetCreditoId(string id)
    {
        _creditoId = id;
        return this;
    }

    public PagoBuilderTest SetCantidad(int cantidad)
    {
        _cantidad = cantidad;
        return this;
    }

    public PagoBuilderTest SetFechaPago(FechaPago fechaPago)
    {
        FechaPago = fechaPago;
        return this;
    }

    public static Pago Build() => new(_id, _usuarioId, _creditoId, _cantidad, FechaPago);
}