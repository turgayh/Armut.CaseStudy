

namespace Armut.CaseStudy.Operation.Helper
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string UserInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string UserInfoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
