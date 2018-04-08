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
using WPFExportSolution.Models;
using WPFExportSolution.SharedFolderManager;

namespace WPFExportSolution
{
    /// <summary>
    /// Interaction logic for SharedFolderWindow.xaml
    /// </summary>
    public partial class SharedFolderWindow : MetroWindow
    {
        SharedFolderCredentials sharedFolderCredentials = SharedFolderCredentials.Instance;
        public SharedFolderWindow()
        {
            InitializeComponent();
            OK_Button.IsEnabled = (SharedFolderManager.SharedFolderManager.Instance.Connected?true:false);
            SetInitialCredentials();
        }

        private void SetInitialCredentials()
        {
            HostName.Text = sharedFolderCredentials.Host;
            SharedFolderUserName.Text = sharedFolderCredentials.UserName;
            SharedFolderPassword.Password = sharedFolderCredentials.Password;
        }
        private void SetCredentials()
        {
            sharedFolderCredentials.Host = HostName.Text;
            sharedFolderCredentials.UserName = SharedFolderUserName.Text;
            sharedFolderCredentials.Password = SharedFolderPassword.Password;
        }

        private void InputChanged(object sender, RoutedEventArgs e)
        {
            bool checkInputChanged = sharedFolderCredentials.Host == HostName.Text &&
                                     sharedFolderCredentials.UserName == SharedFolderUserName.Text &&
                                     sharedFolderCredentials.Password == SharedFolderPassword.Password;
            if (checkInputChanged)
            {
                OK_Button.IsEnabled = true;
            }
            else
            {
                OK_Button.IsEnabled = false;
            }
        }
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            SetCredentials();
            SharedFolderManager.SharedFolderManager sharedFolder = SharedFolderManager.SharedFolderManager.Instance;
            var status = sharedFolder.TestConnection();
            if (status.Item2)
            {
                OK_Button.IsEnabled = true;
                sharedFolder.Connected = true;
                MessageBox.Show(status.Item1);
            }
            else
            {
                MessageBox.Show(status.Item1);
                sharedFolder.Connected = false;
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HostName.Text = "";
            SharedFolderUserName.Text = "";
            SharedFolderPassword.Password = "";
            SetCredentials();
            SharedFolderManager.SharedFolderManager.Instance.Connected = false;
            this.Close();
        }
    }
}
