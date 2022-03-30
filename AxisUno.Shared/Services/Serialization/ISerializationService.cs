// <copyright file="ISerializationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Serialization
{
    using AxisUno.DataBase.Repositories.Serialization;
    using AxisUno.Enums;

    /// <summary>
    /// Describes serialization service.
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Gets or sets serialization value by key.
        /// </summary>
        /// <param name="key">Key to search serialization value.</param>
        /// <returns>SerializationItemModel.</returns>
        /// <date>28.03.2022.</date>
        SerializationItemModel this[ESerializationKeys key] { get; set; }

        /// <summary>
        /// Gets a value indicating whether data is initialized.
        /// </summary>
        /// <date>29.03.2022.</date>
        bool SerializationDataInitialized { get; }

        /// <summary>
        /// Set serialization data property.
        /// </summary>
        /// <param name="group">Key to search serialization value.</param>
        /// <param name="serialization">Class to communicate with database.</param>
        /// <date>28.03.2022.</date>
        void InitSerializationData(ESerializationGroups group, ISerializationRepository serialization);

        /// <summary>
        /// Determines whether the serialization data contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the serialization data.</param>
        /// <returns>Returns true if the serialization data contains an element with the specified key; otherwise returns false.</returns>
        /// <date>28.03.2022.</date>
        bool ContainsKey(ESerializationKeys key);

        /// <summary>
        /// Update serialization values in the database.
        /// </summary>
        /// <date>28.03.2022.</date>
        void Update();
    }
}
