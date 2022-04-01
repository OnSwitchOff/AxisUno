namespace AxisUno.DataBase.My100REnteties.Interfaces
{
    /// <summary>
    /// Describes mandatory properties of class of group of nomenclatures.
    /// </summary>
    public interface INomenclaturesGroups
    {
        /// <summary>
        /// Gets or sets id of group of nomenclatures.
        /// </summary>
        /// <date>01.04.2022.</date>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets path of group of nomenclatures.
        /// </summary>
        /// <date>01.04.2022.</date>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets name of group of nomenclatures.
        /// </summary>
        /// <date>01.04.2022.</date>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets discount of group of nomenclatures.
        /// </summary>
        /// <date>01.04.2022.</date>
        int Discount { get; set; }
    }
}
