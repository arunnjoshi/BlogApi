using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models
{
    public class AppSettings:IAppSettings
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
