using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.BusinessRules
{
    public interface IBusinessRule
    {
        /// <summary>
        /// Error message, that will be shown in case, when rule is broken.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Chek, that business rule is broken.
        /// </summary>
        /// <returns>True, when rule is broken.</returns>
        bool BrokenWhen();
    }
}
