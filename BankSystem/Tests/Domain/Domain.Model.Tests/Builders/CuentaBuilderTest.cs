using Domain.Model.Entities.Cuenta;
using Domain.Model.ValueObjects;

namespace Domain.Model.Tests.Builders;

public class CuentaBuilderTest
{
    private static string _id = string.Empty;
    private static string _usuarioId = string.Empty;
    private static Banco Banco;
    private static int _capacidadEndeudamiento = 0;

    public CuentaBuilderTest()
    {
    }

    public CuentaBuilderTest SetId(string id)
    {
        _id = id;
        return this;
    }

    public CuentaBuilderTest SetUsuarioId(string id)
    {
        _usuarioId = id;
        return this;
    }

    public CuentaBuilderTest SetBanco(Banco banco)
    {
        Banco = banco;
        return this;
    }

    public CuentaBuilderTest SetCapacidadEndeudamiento(int cantidad)
    {
        _capacidadEndeudamiento = cantidad;
        return this;
    }

    public Cuenta Build() => new(_id, _usuarioId, Banco, _capacidadEndeudamiento);
}