using System;
using System.IO;
using System.Net;
using WPFExportSolution.ERPDatabaseManager;
using WPFExportSolution.Models;

namespace WPFExportSolution.SharedFolderManager
{
    class SharedFolderManager : ISharedFolderManager
    {
        public static readonly SharedFolderManager Instance = new SharedFolderManager(); 
        SharedFolderCredentials sharedFolderCredentials = SharedFolderCredentials.Instance;
        DatabaseManager databaseManager = DatabaseManager.Instance; 

        public bool Connected;
        public string ExportCSV()
        {
            var credentials = new NetworkCredential(sharedFolderCredentials.UserName, sharedFolderCredentials.Password);

            try
            {
                using (new NetworkConnection(sharedFolderCredentials.Host, credentials))
                {
                    string filename = String.Format("{0}.{1}",DateTime.Now.ToString("MMMMM_yyyy"),"csv");
                    string filepath = String.Format(@"{0}\{1}", sharedFolderCredentials.Host,filename);
                    File.WriteAllText(filepath,databaseManager.GetEmployeeRecords());
                }
                return "Export Complete";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }

        public (string, bool) TestConnection()
        {
            try
            {
                var credentials = new NetworkCredential(sharedFolderCredentials.UserName, sharedFolderCredentials.Password);
                var test = new NetworkConnection(sharedFolderCredentials.Host, credentials);
                return ("Shared Folder Connection Successful",true);
            }
            catch (Exception ex)
            {

                return (ex.Message, false);
            }
        }
    }
}
