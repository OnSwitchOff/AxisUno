using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.PartnersGroups;
using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.My100REnteties.Partners
{
    public partial class Partner : Entity
    {
        public int Id { get; set; }
        public string Company { get; set; } = null!;
        public string Principal { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string Vatnumber { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string BankBic { get; set; } = null!;
        public string Iban { get; set; } = null!;
        public string DiscountCard { get; set; } = null!;
        public string Email { get; set; } = null!;
        public PartnersGroup Group { get; set; }
        public ENomenclatureStatuses Status { get; set; }
    }
}
