namespace ReportService.Persistence.Configuration
{
    public class DatabaseSettings
    {
        public string UseSettings { get; set; } = string.Empty;
    }
    public class PosgreSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
    public class MangoSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

}
