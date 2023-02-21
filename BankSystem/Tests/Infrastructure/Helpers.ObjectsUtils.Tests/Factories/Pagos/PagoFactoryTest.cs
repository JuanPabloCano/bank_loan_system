using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Entities.Credito;
using Domain.Model.Entities.Pago;
using Domain.Model.Entities.Usuario;
using Domain.Model.Tests.Builders;
using Domain.Model.ValueObjects;
using EntryPoints.ReactiveWeb.Entities.Pagos;

namespace Helpers.ObjectsUtils.Tests.Factories.Pagos
{
    public abstract class PagoFactoryTest
    {
        public static Pago ObtenerPagoTest(string id, string usuarioId, string creditoId, int cantidad) =>
            new PagoBuilderTest()
                .SetId(id)
                .SetUsuarioId(usuarioId)
                .SetCreditoId(creditoId)
                .SetCantidad(cantidad)
                .SetFechaPago(new FechaPago(DateTime.Now))
                .Build();

        public static PagoRequest ObtenerPagoRequestTest(string id, string usuarioId, string creditoId, int cantidad) =>
            new PagoRequest()
            {
                Id = id,
                UsuarioId = usuarioId,
                CreditoId = creditoId,
                Cantidad = cantidad,
                FechaPago = new FechaPago(DateTime.Now)
            };

        public static List<Pago> ListarPagosTest() => new()
        {
            new PagoBuilderTest()
                .SetId("252626")
                .SetUsuarioId("23949805")
                .SetCreditoId("450934723")
                .SetCantidad(6000)
                .SetFechaPago(new FechaPago(DateTime.Now))
                .Build(),
            new PagoBuilderTest()
                .SetId("834273")
                .SetUsuarioId("456556")
                .SetCreditoId("2568909")
                .SetCantidad(2000)
                .SetFechaPago(new FechaPago(DateTime.Now))
                .Build()
        };
    }
}