namespace ContactService.Application.Dto.Report
{
    public class ContactReportByLocationDto
    {
        public string LocationName { get; set; } = string.Empty;
        public long PersonCountInLocation { get; set; }
        public long PhoneNumberCountInLocation { get; set; }
    }
}
