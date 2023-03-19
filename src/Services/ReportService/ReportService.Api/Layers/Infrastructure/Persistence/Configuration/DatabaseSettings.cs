namespace ReportService.Persistence.Configuration
{
    public class DatabaseSettings
    {
        public string DatabaseType { get; set; } = string.Empty;
    }
    public class PosgreSettings : DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
    public class MangoSettings : DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

}
