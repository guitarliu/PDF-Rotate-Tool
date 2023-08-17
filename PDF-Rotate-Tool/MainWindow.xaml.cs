using Microsoft.Win32;
using System;
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
            TbkAnnotion.Visibility = Visibility.Hidden;
            FilePathCmbx.Visibility = Visibility.Hidden;
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
            TbkAnnotion.Visibility = Visibility.Hidden;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "PDF Files|*.pdf";
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
    }
}
