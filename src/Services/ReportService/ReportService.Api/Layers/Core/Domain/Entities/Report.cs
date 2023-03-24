using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ReportService.Domain.Enums;

namespace ReportService.Domain.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public ReportStatusType ReportStatusType { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ReportType ReportType { get; set; }
        public string FilePath { get; set; } = string.Empty;

    }
}
