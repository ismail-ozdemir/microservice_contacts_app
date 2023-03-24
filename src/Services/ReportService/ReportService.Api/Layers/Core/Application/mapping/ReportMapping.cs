using AutoMapper;
using ReportService.Application.Dtos;
using ReportService.Domain.Entities;

namespace ReportService.Application.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {

            CreateMap<Report, ReportDto>()
            .ForMember(t => t.ReportType, opt => opt.MapFrom(s => s.ReportType.ToString()))
            .ForMember(t => t.Status, opt => opt.MapFrom(s => s.ReportStatusType.ToString()))
            .ForMember(t => t.RequestDate, opt => opt.MapFrom(s => s.RequestDate))
            .ForMember(t => t.FilePath, opt => opt.MapFrom(s => s.FilePath))
            .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => s.CompletedDate));

        }
    }
}
