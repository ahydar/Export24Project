using System;
namespace WPFExportSolution.Models
{
    public class DbCredentials
    {
        public static readonly DbCredentials Instance = new DbCredentials();

        private DbCredentials()
        {

        }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string TableName { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                ServerName,
                DatabaseName,
                UserName,
                Password
                );
        }
    }
}
