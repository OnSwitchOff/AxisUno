using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.Documents;
using AxisUno.DataBase.My100REnteties.OperationHeaders.Enums;
using AxisUno.DataBase.My100REnteties.Partners;
using AxisUno.DataBase.My100REnteties.PaymentTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.My100REnteties.OperationHeaders
{
    public partial class OperationHeader: Entity
    {
        public int Id { get; set; }
        public OperType OperType { get; set; }
        public int Acct { get; set; }
        public DateTime Date { get; set; }
        public string Usn { get; set; } = null!;
        public Partner Partner { get; set; }
        public PaymentType Payment { get; set; }
        public string Note { get; set; } = null!;
        public OperationHeader SrcDoc { get; set; }
        public EcrreceiptType EcrreceiptType { get; set; }
        public int EcrreceiptNumber { get; set; }
        public DateTime UserRealTime { get; set; }
        public OperStatus Status { get; set; }

        public List<OperationDetails.OperationDetail> OperationDetails { get; set; } = new List<OperationDetails.OperationDetail>();
    }
}
