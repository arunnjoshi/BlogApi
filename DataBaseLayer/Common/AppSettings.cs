namespace DataBaseLayer.Common
{
    public class DbAppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
        public string JwtKey { get; set; }
    }


    public interface IAppSettings
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
        public string JwtKey { get; set; }

    }
}
