using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Entities.Cuenta;
using Domain.Model.Tests.Builders;
using Domain.Model.ValueObjects;

namespace Helpers.ObjectsUtils.Tests.Factories.Cuentas
{
    public abstract class CuentaFactoryTest
    {
        public static Cuenta ObtenerCuentaTest(string id, string usuarioId, Banco banco, int capacidadEndeudamiento) =>
            new CuentaBuilderTest()
                .SetId(id)
                .SetUsuarioId(usuarioId)
                .SetBanco(banco)
                .SetCapacidadEndeudamiento(capacidadEndeudamiento)
                .Build();
    }
}