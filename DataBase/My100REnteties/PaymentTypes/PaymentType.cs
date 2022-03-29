using AxisUno.DataBase.Enteties;
using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.My100REnteties.PaymentTypes
{
    public partial class PaymentType : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public EPaymentTypes PaymentIndex { get; set; }
    }
}
