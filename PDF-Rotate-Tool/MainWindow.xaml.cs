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

            // Set RegisterWindow's machineID_str with EncryptMachineID
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.machineID_str = EncryptMachineID(Get_MachineID());

            // Read Register File to check whether RegisterCode is matched with MachineID
            string registerFilePath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\Register.txt";
            if (!File.Exists(registerFilePath) || new FileInfo(registerFilePath).Length == 0)
            {
                using (StreamWriter writer = new StreamWriter(registerFilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(EncryptMachineID(Get_MachineID()) + "\n");
                }
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
            Window registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
        private void Author_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.notion.so/55a9daf124ea47b18d0322aca011d4dd?pvs=4") { UseShellExecute = true });
        }

        /// <summary>
        /// Get CPUID、MotherboardID、DiskID、BIOSID and combine them into MachineID
        /// Write MachineID into Register.txt 
        /// Check whether RegisterCode is matched MachineID
        /// </summary>
        /// <returns></returns>
        private static string Get_MachineID()
        {
            string machineID = null;

            // Get CPUID
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                machineID += mo.Properties["ProcessorId"].Value.ToString();
                break;
            }

            // Get DiskID
            ManagementClass mc3 = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc3 = mc3.GetInstances();
            foreach (ManagementObject mo in moc3)
            {
                machineID += mo.Properties["SerialNumber"].Value.ToString().Replace(" ", "");
                break;
            }

            // Get Mac Address
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    machineID += networkInterface.GetPhysicalAddress().ToString();
                    break; 
                }
            }

            return machineID;
        }
        /// <summary>
        /// Encrypt MachineID
        /// </summary>
        /// <param name="machineID"></param>
        /// <returns></returns>
        private static string EncryptMachineID(string machineID)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] randomKey = new byte[16];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) 
                {
                    rng.GetBytes(randomKey);
                }

                byte[] randomIV = new byte[16];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomIV);
                }

                aesAlg.Key = randomKey;
                aesAlg.IV = randomIV;

                // Create A encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create A MemoryStream
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Create A CryptoStream
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write Machine to CryptoStream
                            swEncrypt.Write(machineID);
                        }
                    }
                    // Return Encrypt Data
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
