using Domain.Model.Entities.Credito;
using Domain.Model.Tests.Builders;

namespace Helpers.ObjectsUtils.Tests.Factories.Creditos
{
    public abstract class CreditoFactoryTest
    {
        public static Credito ObtenerCredito(string id, string usuarioId, string cuentaId, int totalPrestamo,
            int deudaActual) =>
            new CreditoBuilderTest()
                .SetId(id)
                .SetUsuarioId(usuarioId)
                .SetCuentaId(cuentaId)
                .SetTotalPrestamo(totalPrestamo)
                .SetDeudaActual(deudaActual)
                .Build();
    }
}