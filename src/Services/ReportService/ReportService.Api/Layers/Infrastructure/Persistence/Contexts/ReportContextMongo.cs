using MongoDB.Driver;
using ReportService.Domain.Entities;
using ReportService.Persistence.Configuration;

namespace ReportService.Persistence.Contexts
{
    public class ReportContextMongo
    {
        //TODO : policy retry
        public ReportContextMongo(MangoSettings setting)
        {

            var client = new MongoClient(setting.ConnectionString);
            var database = client.GetDatabase(setting.DatabaseName);
            Reports = database.GetCollection<Report>("Reports");
        }
        public IMongoCollection<Report> Reports { get; }
    }
}
