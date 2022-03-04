using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Registration
{
    public class Registration
    {
        private Registration()
        {
            // This constructor is needed only for ef core
        }

        private Registration(
            string company,
            string principal,
            string city,
            string address,
            string phone,
            string taxNumber,
            string vatNumber,
            string iBan)
        {
            Company = company;
            Principal = principal;
            City = city;
            Address = address;
            Phone = phone;
            TaxNumber = taxNumber;
            VATNumber = vatNumber;
            IBAN = iBan;
        }

        /// <summary>
        /// Gets or sets organization name.
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets financially responsible person.
        /// </summary>
        public string Principal { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string TaxNumber { get; set; } = string.Empty;

        public string VATNumber { get; set; } = string.Empty;

        public string IBAN { get; set; } = string.Empty;

        public static Registration Create(
            string company = "",
            string principal = "",
            string city = "",
            string address = "",
            string phone = "",
            string taxNumber = "",
            string vatNumber = "",
            string iBAN = "")
        {
            return new Registration(company, principal, city, address, phone, taxNumber, vatNumber, iBAN);
        }
    }
}
