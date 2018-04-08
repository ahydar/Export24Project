using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Media;
using WPFExportSolution.ERPDatabaseManager;
using WPFExportSolution.Models;
using WPFExportSolution.SharedFolderManager;

namespace WPFExportSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DbCredentials dbCredentials = DbCredentials.Instance;
        public MainWindow()
        {
            InitializeComponent();
            ExportButton.IsEnabled = false;
        }

        private void DbConnection(object sender, RoutedEventArgs e)
        {
            DatabaseWindow dbWindow = new DatabaseWindow();
            dbWindow.ShowDialog();
            if (DatabaseManager.Instance.Connected)
            {
                DatabaseButton.Background = new LinearGradientBrush(Color.FromRgb(92, 184, 92), Color.FromRgb(92, 184, 92), 0);
            }
            else
            {
                DatabaseButton.Background = new LinearGradientBrush(Color.FromRgb(217, 83, 79), Color.FromRgb(217, 83, 79), 0);
            }
            IsExportReady();
        }

        private void SharedFolderConnection(object sender, RoutedEventArgs e)
        {
            SharedFolderWindow sharedFolderWindow = new SharedFolderWindow();
            sharedFolderWindow.ShowDialog();
            if (SharedFolderManager.SharedFolderManager.Instance.Connected)
            {
                SharedFolderButton.Background = new LinearGradientBrush(Color.FromRgb(92, 184, 92), Color.FromRgb(92, 184, 92), 0);
            }
            else
            {
                SharedFolderButton.Background = new LinearGradientBrush(Color.FromRgb(217, 83, 79), Color.FromRgb(217, 83, 79), 0);
            }
            IsExportReady();
        }

        private void IsExportReady()
        {
            if(SharedFolderManager.SharedFolderManager.Instance.Connected && DatabaseManager.Instance.Connected)
            {
                ExportButton.IsEnabled = true;
            }
            else
            {
                ExportButton.IsEnabled = false;
            }
        }
        private void BeginExport(object sender, RoutedEventArgs e)
        {
            DatabaseManager Db = DatabaseManager.Instance;
            var conn = Db.OpenConnection();
            if (conn.Item2)
            {
                SharedFolderManager.SharedFolderManager sharedFolder = SharedFolderManager.SharedFolderManager.Instance;
                string status = sharedFolder.ExportCSV();
                MessageBox.Show(status);
                Db.CloseConnection();
            }
            else
            {
                MessageBox.Show(conn.Item1);
            }
        }
    }
}
