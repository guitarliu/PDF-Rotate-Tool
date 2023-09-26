using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace PDF_Rotate_Tool
{
    /// <summary>
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {
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

        private async void Init_RegisterWindow()
        {
            // Create an instance of ResourceManager, specifying the name of the resource file and the assembly that contains it
            ResourceManager resourceManager = new ResourceManager("PDF_Rotate_Tool.ResourcePublicKey", typeof(ResourcePublicKey).Assembly);

            string registerFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Register.txt";

            // Retrieve the string using the GetString method, where "publickey" is the key for the string you set in ResourcePublicKey.resx
            string publickey = resourceManager.GetString("publickey");

            // Get RegisterCode from RegisterWindow
            string activationCode = Tbx_RegisterCode.Text;

            // Verify activationCode
            bool isValid = VerifyActivationCode(Tbx_MachineID.Text, publickey, activationCode);

            if (isValid)
            {
                // Activate Program
                MessageBox.Show("注册成功!", "注册信息", MessageBoxButton.OK, MessageBoxImage.Information);

                // Write RegisterCode to Register.txt
                if (File.Exists(registerFilePath) && new FileInfo(registerFilePath).Length != 0)
                {
                    string[] lines = File.ReadAllLines(registerFilePath);
                    if (lines.Length >= 2)
                    {
                        lines[1] = Tbx_RegisterCode.Text;
                        File.WriteAllLines(registerFilePath, lines);
                    }
                }
            }
            else
            {
                // Failure Activating Program 
                MessageBox.Show("注册码错误, 注册失败!!", "注册信息", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();

                // Trigger the delegate event to close the main window
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.Close();
                }
            }
        }

        public static bool VerifyActivationCode(string machineID, string publicKey, string activationCode)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(publicKey);

                    // Convert machineID to bytes
                    byte[] machineIDBytes = Encoding.UTF8.GetBytes(machineID);

                    // Convert activatationCode's Base64 string to bytes
                    byte[] signatureBytes = Convert.FromBase64String(activationCode);

                    bool isSignatureValid = rsa.VerifyData(machineIDBytes, new SHA256CryptoServiceProvider(), signatureBytes);

                    return isSignatureValid;
                }
            }
            catch
            {
                // Verify Failure
                return false;
            }
        }


        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            Init_RegisterWindow();
            this.Close();
        }

        private void Bt_How_Register_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://guitarliutop.notion.site/guitarliutop/PDF-Rotate-Tool-55a9daf124ea47b18d0322aca011d4dd") { UseShellExecute = true });
        }

        private void Bt_Ok_Click(object sender, RoutedEventArgs e)
        {
            // Check whether Register Code is matched MachineID
            // True, Close this window
            // False, Show Alert MessageBox
            Init_RegisterWindow();
            this.Close();
        }

        private void Bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Init_RegisterWindow();
            this.Close();
        }
    }
}
