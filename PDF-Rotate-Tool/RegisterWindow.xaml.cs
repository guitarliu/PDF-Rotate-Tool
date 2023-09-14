using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Resources;
using System.Security.Cryptography;
using System.Text;

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

        private async void Init_RegisterWindow()
        {
            // Create an instance of ResourceManager, specifying the name of the resource file and the assembly that contains it
            ResourceManager resourceManager = new ResourceManager("PDF_Rotate_Tool.ResourcePublicKey", typeof(ResourcePublicKey).Assembly);

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
            }
            else
            {
                // Failure Activating Program 
                MessageBox.Show("注册失败!!", "注册失败", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();

                // Trigger the delegate event to close the main window
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.CloseMainWindowEvent?.Invoke();
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

                    // Create an instance of SHA256
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        // Compute the hash of the machineIDBytes
                        byte[] hashBytes = sha256.ComputeHash(machineIDBytes);

                        // Use RSA public key to verify the signature
                        bool isSignatureValid = rsa.VerifyData(hashBytes, CryptoConfig.MapNameToOID("SHA256"), signatureBytes);

                        return isSignatureValid;
                    }
                }

            }
            catch (CryptographicException ex)
            {
                // Verify Failure
                Console.WriteLine("注册码无效: " + ex.Message);
                return false;
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
            Init_RegisterWindow();
        }

        private void Bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
