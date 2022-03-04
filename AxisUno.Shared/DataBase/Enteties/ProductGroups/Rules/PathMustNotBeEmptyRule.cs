

using AxisUno.BusinessRules;

namespace AxisUno.DataBase.Enteties.ProductGroups.Rules
{
    internal class PathMustNotBeEmptyRule : IBusinessRule
    {
        private readonly string _path;

        public PathMustNotBeEmptyRule(string path)
        {
            _path = path;
        }

        /// <inheritdoc/>
        public string Message => "Product name can't be empty";

        /// <summary>
        /// Checks, new product name, that can't be empty
        /// </summary>
        /// <returns>True, if name is null or empty</returns>
        public bool BrokenWhen()
        {
            return string.IsNullOrEmpty(_path);
        }
    }
}