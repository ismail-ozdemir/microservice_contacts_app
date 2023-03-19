using ReportService.Persistence.Configuration;

namespace ReportService.Persistence.Contexts
{
    public class ReportContextMongo
    {
        private readonly MangoSettings _setting;

        public ReportContextMongo(MangoSettings setting)
        {
            _setting = setting;
        }
    }
}
