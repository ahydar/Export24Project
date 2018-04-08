using WPFExportSolution.Models;

namespace WPFExportSolution.SharedFolderManager
{
    interface ISharedFolderManager
    {
        string ExportCSV();

        (string, bool) TestConnection();
    }
}
