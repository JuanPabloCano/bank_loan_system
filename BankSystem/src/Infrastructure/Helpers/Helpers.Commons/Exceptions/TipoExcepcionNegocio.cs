using System.ComponentModel;

namespace Helpers.Commons.Exceptions
{
    /// <summary>
    /// ResponseError
    /// </summary>
    public enum TipoExcepcionNegocio
    {
        /// <summary>
        /// Tipo de excepcion no controlada
        /// </summary>
        [Description("La entidad no puede ser nula")]
        ErrorEntidadInvalida = 100,

        /// <summary>
        /// Tipo de excepcion no controlada
        /// </summary>
        [Description("La entidad especificada no existe")]
        ErrorEntidadNoEncontrada = 110,

        /// <summary>
        /// Tipo de excepcion no controlada
        /// </summary>
        [Description("El usuario debe de ser mayor de edad")]
        ErrorEdadEntidadIlegal = 115,

        /// <summary>
        /// Tipo de excepcion no controlada
        /// </summary>
        [Description("Excepción de negocio no controlada")]
        ExceptionNoControlada = 555,

        /// <summary>
        /// Tipo de excepcion no controlada
        /// </summary>
        [Description("La capacidad de endeudamiento no puede ser igual o menor que 0")]
        ErrorCapacidadEndeudamientoMenorOIgualCero = 120,

        /// <summary>
        /// Tipo de excepcion no controlada
        /// </summary>
        [Description("La deuda actual ya se encuentra pagada")]
        ErrorDeudaActualPagada = 130,

        /// <summary>
        ///  Tipo de excepcion no controlada
        /// </summary>
        [Description("La deuda actual supera la capacidad de endeudamiento")]
        ErrorCapacidadEndeudamientoInvalida = 140,
    }
}