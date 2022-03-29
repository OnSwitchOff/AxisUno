using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.Partners;
using AxisUno.DataBase.My100REnteties.PaymentTypes;
using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.My100REnteties.OperationHeaders
{
    public partial class OperationHeader: Entity
    {
        public int Id { get; set; }
        public EOperTypes OperType { get; set; }
        public int Acct { get; set; }
        public DateTime Date { get; set; }
        public string Usn { get; set; } = null!;
        public Partner Partner { get; set; }
        public PaymentType Payment { get; set; }
        public string Note { get; set; } = null!;
        public OperationHeader SrcDoc { get; set; }
        public EECCheckTypes EcrreceiptType { get; set; }
        public int EcrreceiptNumber { get; set; }
        public DateTime UserRealTime { get; set; }
        public EOperationModes Status { get; set; }

        public List<OperationDetails.OperationDetail> OperationDetails { get; set; } = new List<OperationDetails.OperationDetail>();
    }
}
