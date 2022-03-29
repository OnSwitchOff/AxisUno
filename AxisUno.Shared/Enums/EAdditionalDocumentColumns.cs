// <copyright file="EAdditionalDocumentColumns.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Enums
{
    /// <summary>
    /// Additional columns in the document table.
    /// </summary>
    public enum EAdditionalDocumentColumns : int
    {
        /// <summary>
        /// Column named "City".
        /// </summary>
        City = 1 << 1,

        /// <summary>
        /// Column named "Address".
        /// </summary>
        Address = 1 << 2,

        /// <summary>
        /// Column named "Phone".
        /// </summary>
        Phone = 1 << 3,
    }
}
