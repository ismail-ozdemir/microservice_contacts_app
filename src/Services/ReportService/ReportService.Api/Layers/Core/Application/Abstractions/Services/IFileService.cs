namespace ReportService.Application.Abstractions.Services
{
    public interface IFileService
    {
         string SaveDataExcelFormat<T>(List<T> data, string folderPath, string fileName);

    }
}
