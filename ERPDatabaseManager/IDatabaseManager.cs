namespace WPFExportSolution.ERPDatabaseManager
{
    interface IDatabaseManager
    {
        (string,bool) OpenConnection();
        void CloseConnection();
        string GetEmployeeRecords();
    }
}
