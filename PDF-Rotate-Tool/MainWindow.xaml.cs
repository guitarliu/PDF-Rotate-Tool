using Microsoft.Win32;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Resources;

namespace PDF_Rotate_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CheckRegisterInfo();
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

        private void Bt_Minimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Bt_Maximum_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void PDFList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!PDFslbx.Items.Contains(file))
                        {
                            // Add the PDF file path to ListBoxItem
                            PDFslbx.Items.Add(file);
                        }
                    }
                }
            }
        }

        private void PDFList_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Multiselect = true;
            dialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (dialog.ShowDialog() == true)
            {
                string[] files = dialog.FileNames;
                // open folder etc.
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!PDFslbx.Items.Contains(file))
                        {
                            // Add the PDF file path to ListBoxItem
                            PDFslbx.Items.Add(file);
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            while (PDFslbx.SelectedIndex != -1)
            {
                PDFslbx.Items.Remove(PDFslbx.SelectedItem);
            }
        }

        private void OpenFile_Menu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] files = openFileDialog.FileNames;
                // open folder etc.
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!PDFslbx.Items.Contains(file))
                        {
                            // Add the PDF file path to ListBoxItem
                            PDFslbx.Items.Add(file);
                        }
                    }
                }
            }
        }
        private void Close_Menu_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// For PDFSharp
        /// "A positive angle indicates clockwise rotation"
        /// "A negative angle indicates counter-clockwise rotation"
        /// PDF's Rotate angle include 0,90,180,270
        /// Rotation Angles must be -90,0,90,180,270
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClockWise90Rotate_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in PDFslbx.Items)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                try
                {
                    PdfDocument document = PdfReader.Open(item, PdfDocumentOpenMode.Modify);
                    foreach (PdfPage page in document.Pages)
                    {
                        page.Rotate += 90;

                    }
                    document.Save(item);
                }
                catch
                {
                    MessageBox.Show("选择文件不存在或有误!");
                }
            }
        }

        private void CounterClockWise90Rotate_Click(object obj, RoutedEventArgs e)
        {
            foreach (string item in PDFslbx.Items)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                try
                {
                    PdfDocument document = PdfReader.Open(item, PdfDocumentOpenMode.Modify);
                    foreach (PdfPage page in document.Pages)
                    {
                        page.Rotate += -90;

                    }
                    document.Save(item);
                }
                catch
                {
                    MessageBox.Show("选择文件不存在或有误!");
                }
            }
        }

        private void Rotate180_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in PDFslbx.Items)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                try
                {
                    PdfDocument document = PdfReader.Open(item, PdfDocumentOpenMode.Modify);
                    foreach (PdfPage page in document.Pages)
                    {
                        page.Rotate += 180;

                    }
                    document.Save(item);
                }
                catch
                {
                    MessageBox.Show("选择文件不存在或有误!");
                }
            }
        }

        private void AutoHorizonalRotate_Click(Object sender, RoutedEventArgs e)
        {
            foreach (string item in PDFslbx.Items)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                try
                {
                    PdfDocument document = PdfReader.Open(item, PdfDocumentOpenMode.Modify);
                    foreach (PdfPage page in document.Pages)
                    {
                        if (page.Rotate != 90)
                        {
                            page.Rotate = 90;
                        }
                    }
                    document.Save(item);
                }
                catch
                {
                    MessageBox.Show("选择文件不存在或有误!");
                }
            }
        }

        private void AutoVerticalRotate_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in PDFslbx.Items)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                try
                {
                    PdfDocument document = PdfReader.Open(item, PdfDocumentOpenMode.Modify);
                    foreach (PdfPage page in document.Pages)
                    {
                        if (page.Rotate != 0)
                        {
                            page.Rotate = 0;
                        }
                    }
                    document.Save(item);
                }
                catch
                {
                    MessageBox.Show("选择文件不存在或有误!");
                }
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Window helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void Register_Click(object sender, RoutedEventArgs e) 
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Tbx_MachineID.Text = Get_MachineID().GetAwaiter().GetResult();
            registerWindow.ShowDialog();
        }
        private void Author_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.notion.so/55a9daf124ea47b18d0322aca011d4dd?pvs=4") { UseShellExecute = true });
        }

        // Get CPUID
        private static async Task<string> GetCPUIDAsync()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                return mo.Properties["ProcessorId"].Value.ToString();
            }
            return string.Empty;
        }

        // Get DiskID
        private static async Task<string> GetDiskIDAsync()
        {
            ManagementClass mc3 = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc3 = mc3.GetInstances();
            foreach (ManagementObject mo in moc3)
            {
                return mo.Properties["SerialNumber"].Value.ToString().Replace(" ", "");
            }
            return string.Empty;
        }

        // Get Mac Address
        private static async Task<string> GetMacAddressAsync()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    return networkInterface.GetPhysicalAddress().ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get CPUID、MotherboardID、DiskID、BIOSID and combine them into MachineID
        /// Write MachineID into Register.txt 
        /// Check whether RegisterCode is matched MachineID
        /// </summary>
        /// <returns></returns>
        private static async Task<string> Get_MachineID()
        {
            string machineID = "";

            // Get CPUID asynchronously
            machineID += await GetCPUIDAsync();

            // Get DiskID asynchronously
            machineID += await GetDiskIDAsync();

            // Get Mac Address asynchronously
            machineID += await GetMacAddressAsync();

            return machineID;
        }

        private async void Init_RegisterWindow()
        {
            // Delay Opration, wait 5 seconds
            await Task.Delay(TimeSpan.FromSeconds(1));

            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Tbx_MachineID.Text = Get_MachineID().GetAwaiter().GetResult();
            registerWindow.ShowDialog();

            // Create an instance of ResourceManager, specifying the name of the resource file and the assembly that contains it
            ResourceManager resourceManager = new ResourceManager("PDF_Rotate_Tool.ResourcePublicKey", typeof(ResourcePublicKey).Assembly);

            // Retrieve the string using the GetString method, where "publickey" is the key for the string you set in ResourcePublicKey.resx
            string publickey = resourceManager.GetString("publickey");

            // Get RegisterCode from RegisterWindow
            string activationCode = registerWindow.Tbx_RegisterCode.Text;

            // Verify activationCode
            bool isValid = VerifyActivationCode(Get_MachineID().GetAwaiter().GetResult(), publickey, activationCode);

            if (isValid)
            {
                // Activate Program
                MessageBox.Show("注册成功!", "注册信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Failure Activating Program 
                MessageBox.Show("注册失败!!", "注册信息", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void CheckRegisterInfo()
        {
            // Read Register File to check whether RegisterCode is matched with MachineID
            string registerFilePath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\Register.txt";
            if (!File.Exists(registerFilePath) || new FileInfo(registerFilePath).Length == 0)
            {
                using (StreamWriter writer = new StreamWriter(registerFilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(Get_MachineID().GetAwaiter().GetResult() + "\n");
                }

                Init_RegisterWindow();
            }
            else
            {
                /* 
                 * Check whether current MachineID is matched Register.txt's MachineID
                 * If Matched, then Check whether MachineID is matched RegisterCode
                 * If Not Matched, then Replace Register.txt's MachineID and 
                 * reinput registercode to Active
                */
            }
        }

        /// <summary>
        /// Verify ActivationCode using publicKey and actiationCode
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="activationCode"></param>
        /// <returns></returns>
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
                return false;
            }
        }
    }
}
