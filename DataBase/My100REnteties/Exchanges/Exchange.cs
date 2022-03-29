using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.OperationHeaders;
using DataBase.My100REnteties.Exchanges.Enums;
using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.My100REnteties.Exchanges
{
    public partial class Exchange: Entity
    {
        public int Id { get; set; }
        public OperationHeader OperationHeader { get; set; }
        public EExchangeDirections ExchangeType { get; set; }
        public string AppName { get; set; } = null!;
        public string AppKey { get; set; } = null!;
        public int Acct { get; set; }
        public EOperTypes OperType { get; set; }
    }
}
