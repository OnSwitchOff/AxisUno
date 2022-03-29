using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.OperationDetails
{
    public partial class OperationDetail: Entity
    {
        public int Id { get; set; }
        public OperationHeaders.OperationHeader OperationHeader { get; set; }
        public Items.Item Good { get; set; }
        public decimal Qtty { get; set; }
        public int Sign { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchaseVat { get; set; }
        public decimal SaleVat { get; set; }
        public int SrcId { get; set; }
        public string Note { get; set; } = null!;
    }
}
