using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Partners
{
    public class Partner : Entity
    {
        /// <summary>
        /// This constructor is needed only for EF Core.
        /// </summary>
        private Partner()
        {
        }

        public int Id { get; private set; } = 1;

        public string Company { get; private set; } = string.Empty;

        public string Principal { get; private set; } = string.Empty;

        public string City { get; private set; } = string.Empty;

        public string Address { get; private set; } = string.Empty;

        public string Phone { get; private set; } = string.Empty;

        public string TaxNumber { get; private set; } = string.Empty;

        public string VATNumber { get; private set; } = string.Empty;

        public string BankName { get; private set; } = string.Empty;

        public string BankBIC { get; private set; } = string.Empty;

        public string IBAN { get; private set; } = string.Empty;

        public string DiscountCard { get; private set; } = string.Empty;

        public string Mail { get; set; } = string.Empty;

        public int GroupId { get; set; }

        public int Status { get; set; }
    }
}
