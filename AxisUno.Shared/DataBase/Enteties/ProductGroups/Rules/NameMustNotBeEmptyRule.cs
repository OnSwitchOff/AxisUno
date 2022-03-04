

using AxisUno.BusinessRules;

namespace AxisUno.DataBase.Enteties.ProductGroups.Rules
{
    /// <summary>
    /// A business rule, that guarantees, that a new product name won't be empty.
    /// </summary>
    internal class NameMustNotBeEmptyRule : IBusinessRule
    {
        private readonly string _name;

        public NameMustNotBeEmptyRule(string name)
        {
            _name = name;
        }

        /// <inheritdoc/>
        public string Message => "Product name can't be empty";

        /// <summary>
        /// Checks, new product name, that can't be empty
        /// </summary>
        /// <returns>True, if name is null or empty</returns>
        public bool BrokenWhen()
        {
            return string.IsNullOrEmpty(_name);
        }
    }
}