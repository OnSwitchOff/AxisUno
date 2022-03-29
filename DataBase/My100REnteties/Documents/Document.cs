using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.OperationHeaders;
using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.My100REnteties.Documents
{
    public class Document : Entity
    {
        public int Id { get; set; }
        public OperationHeader OperationHeader { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public EDocumentTypes DocumentType { get; set; }
        public DateTime TaxDate { get; set; }
        public string? SourceDocumentNumber { get; set; }
        public DateTime? SourceDocumentDate { get; set; }
        public string RecipientName { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public string DealDescription { get; set; } = null!;
        public string DealLocation { get; set; } = null!;

        public Document()
        {

        }

        public Document(OperationHeader operationHeader, string documentNumber, DateTime documentDate, EDocumentTypes documentType, DateTime taxDate, string? sourceDocumentNumber, DateTime? sourceDocumentDate, string recipientName, string creatorName, string dealDescription, string dealLocation)
        {
            OperationHeader = operationHeader;
            DocumentNumber = documentNumber;
            DocumentDate = documentDate;
            DocumentType = documentType;
            TaxDate = taxDate;
            SourceDocumentNumber = sourceDocumentNumber;
            SourceDocumentDate = sourceDocumentDate;
            RecipientName = recipientName;
            CreatorName = creatorName;
            DealDescription = dealDescription;
            DealLocation = dealLocation;
        }

        public static Document Create(OperationHeader operationHeader, string documentNumber, DateTime documentDate, EDocumentTypes documentType, DateTime taxDate, string? sourceDocumentNumber, DateTime? sourceDocumentDate, string recipientName, string creatorName, string dealDescription, string dealLocation)
        {
            //CheckRule(new DescriptionMustNotBeEmptyRule(description));

            Document document = new Document(operationHeader, documentNumber, documentDate, documentType, taxDate, sourceDocumentNumber, sourceDocumentDate, recipientName, creatorName, dealDescription, dealLocation)
            {

            };
            return document;
        }
    }
}
