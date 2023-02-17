using Domain.Model.ValueObjects;
using DrivenAdapters.Mongo.Entities.Cuentas;

namespace DrivenAdapters.Mongo.Tests.Builders;

public class CuentaDataBuilderTest
{
    private static string _id = string.Empty;
    private static string _usuarioId = string.Empty;
    private static Banco Banco;
    private static int _capacidadEndeudamiento = 0;

    public CuentaDataBuilderTest()
    {
    }

    public CuentaDataBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public CuentaDataBuilderTest SetUsuarioId(string id)
    {
        _usuarioId = id;
        return this;
    }

    public CuentaDataBuilderTest SetBanco(Banco banco)
    {
        Banco = banco;
        return this;
    }

    public CuentaDataBuilderTest SetCapacidadEndeudamiento(int cantidad)
    {
        _capacidadEndeudamiento = cantidad;
        return this;
    }

    public static CuentaData Build() => new(_id, _usuarioId, Banco, _capacidadEndeudamiento);
}