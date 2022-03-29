using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.Settings
{
    public partial class Setting : Entity
    {
        public int Id { get; set; }
        public string Group { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
