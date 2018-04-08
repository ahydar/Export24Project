using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFExportSolution.ERPDatabaseManager;
using WPFExportSolution.Models;

namespace WPFExportSolution
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class DatabaseWindow : MetroWindow
    {
        DbCredentials dbCredentials = DbCredentials.Instance;
        public DatabaseWindow()
        {
            InitializeComponent();
            OK_Button.IsEnabled = (DatabaseManager.Instance.Connected?true:false);
            SetInitialDatabaseCredentials();
        }

        public void SetInitialDatabaseCredentials()
        {
            ServerName.Text = dbCredentials.ServerName;
            DatabaseName.Text = dbCredentials.DatabaseName;
            TableName.Text = dbCredentials.TableName;
            DbUserName.Text = dbCredentials.UserName;
            DbPassword.Password = dbCredentials.Password;
        }

        public void SetDatabaseCredentials()
        {
            dbCredentials.ServerName = ServerName.Text;
            dbCredentials.DatabaseName = DatabaseName.Text;
            dbCredentials.TableName = TableName.Text;
            dbCredentials.UserName = DbUserName.Text;
            dbCredentials.Password = DbPassword.Password;
        }

        private void DbCredentialsTest(object sender, RoutedEventArgs e)
        {
            SetDatabaseCredentials();
            DatabaseManager Db = DatabaseManager.Instance;
            (string, bool) status = Db.OpenConnection();
            if (status.Item2)
            {
                OK_Button.IsEnabled = true;
                Db.Connected = true;
            }
            else
            {
                Db.Connected = false;
            }
            MessageBox.Show(status.Item1);
            Db.CloseConnection();
        }

        private void InputChanged(object sender, KeyEventArgs e)
        {
            bool checkInputChanged = ServerName.Text == dbCredentials.ServerName &&
                         DatabaseName.Text == dbCredentials.DatabaseName &&
                         TableName.Text == dbCredentials.TableName &&
                         DbUserName.Text == dbCredentials.UserName &&
                         DbPassword.Password == dbCredentials.Password;
            if (checkInputChanged)
            {
                OK_Button.IsEnabled = true;
            }
            else
            {
                OK_Button.IsEnabled = false;
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            ServerName.Text = "";
            DatabaseName.Text = "";
            TableName.Text = "";
            DbUserName.Text = "";
            DbPassword.Password = "";
            SetDatabaseCredentials();
            DatabaseManager.Instance.Connected = false;
            this.Close();
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
