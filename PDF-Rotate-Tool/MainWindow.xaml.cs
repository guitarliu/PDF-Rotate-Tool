using Microsoft.Win32;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PDF_Rotate_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class PdfFile
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
        public MainWindow()
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
                    if (System.IO.Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
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
                    if (System.IO.Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
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
                    if (System.IO.Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
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
    }
}
