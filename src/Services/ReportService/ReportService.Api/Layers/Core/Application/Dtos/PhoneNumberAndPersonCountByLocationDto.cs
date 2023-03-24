namespace ReportService.Application.Dtos
{
    public class PhoneNumberAndPersonCountByLocationDto
    {

        public PhoneNumberAndPersonCountByLocationDto(string locationName, long personCountInLocation, long phoneNumberCountInLocation)
        {
            LocationName = locationName;
            PersonCountInLocation = personCountInLocation;
            PhoneNumberCountInLocation = phoneNumberCountInLocation;
        }
        public PhoneNumberAndPersonCountByLocationDto()
        {

        }

        public string LocationName { get; set; } = string.Empty;
        public long PersonCountInLocation { get; set; }
        public long PhoneNumberCountInLocation { get; set; }
    }
}
