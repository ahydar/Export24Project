namespace WPFExportSolution.Models
{
    public class SharedFolderCredentials
    {
        public static readonly SharedFolderCredentials Instance = new SharedFolderCredentials();

        private SharedFolderCredentials()
        {

        }
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
