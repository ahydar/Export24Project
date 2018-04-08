using System;
using System.Data.SqlClient;
using System.Text;
using WPFExportSolution.Models;

namespace WPFExportSolution.ERPDatabaseManager
{
    public class DatabaseManager : IDatabaseManager
    {
        public static readonly DatabaseManager Instance = new DatabaseManager();

        public bool Connected;

        private SqlConnection DbConnection;

        private DbCredentials dbCredentials = DbCredentials.Instance;

        private DatabaseManager()
        {

        }
        public (string,bool) OpenConnection()
        {
            DbConnection = new SqlConnection(dbCredentials.ToString());
            try
            {
                DbConnection.Open();
                return ("Database Connection Successful",true);
            }
            catch (Exception ex)
            {

                return (ex.Message, false);
            }
            
        }
        public void CloseConnection()
        {
            DbConnection.Close();
        }

        public string GetEmployeeRecords()
        {
            string sql = String.Format("select * from {0}",DbCredentials.Instance.TableName);
            StringBuilder CSVData = new StringBuilder();
            try
            {
                var command = new SqlCommand(sql, DbConnection);
                var results = command.ExecuteReader();
                int totalCols = 0;

                StringBuilder row = new StringBuilder();
                while (results.Read())
                {

                    totalCols = results.FieldCount;
                    for(int i = 0; i < totalCols; i++)
                    {
                        if (i != totalCols - 1)
                            row.Append(results.GetValue(i)).Append(",");
                        else
                            row.Append(results.GetValue(i));
                    }
                    CSVData.AppendLine(row.ToString());
                    row.Clear();
                }
                results.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            return CSVData.ToString();
        }
    }
}
