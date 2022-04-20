// <copyright file="IValidationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Validation
{
    /// <summary>
    /// Methods to check values.
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Check whether string value is numeric.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>Returns true if string value is numeric; otherwise returns false.</returns>
        /// <date>20.04.2022.</date>
        bool IsNumeric(string value);

        /// <summary>
        /// Check whether string value is decimal.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>Returns true if string value is decimal; otherwise returns false.</returns>
        /// <date>20.04.2022.</date>
        bool IsDecimalValue(string value);
    }
}
