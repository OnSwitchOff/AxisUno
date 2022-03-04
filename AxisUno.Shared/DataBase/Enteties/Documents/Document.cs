using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Documents
{
    public class Document : Entity
    {
        /// <summary>
        /// This constructor is needed only for EF Core.
        /// </summary>
        private Document()
        {
        }

        public int Id { get; private set; }

        public int OperationHeaderID { get; private set; }

        public string DocumentNumber { get; private set; } = string.Empty;

        public DateTime DocumentDate { get; private set; }

        public int DocumentType { get; private set; }

        public DateTime TaxDate { get; private set; }

        public string SourceDocumentNumber { get; private set; } = string.Empty;

        public DateTime SourceDocumentDate { get; private set; }

        public string RecipientName { get; private set; } = string.Empty;

        public string CreatorName { get; private set; } = string.Empty;

        public string DealDescription { get; private set; } = string.Empty;
    }
}
