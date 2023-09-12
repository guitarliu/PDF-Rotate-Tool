using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PDF_Rotate_Tool
{
    /// <summary>
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public string machineID_str { get; set; }
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Bt_How_Register_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.notion.so/55a9daf124ea47b18d0322aca011d4dd?pvs=4") { UseShellExecute = true });
        }

        private void Bt_Ok_Click(object sender, RoutedEventArgs e)
        {
            // Check whether Register Code is matched MachineID
            // True, Close this window
            // False, Show Alert MessageBox
        }

        private void Bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
