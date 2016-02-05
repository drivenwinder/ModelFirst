using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Configuration;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using ICCEmbedded.SharpZipLib.Zip;

namespace AutoUpdate
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Updator updator;

        public MainWindow()
        {
            InitializeComponent();
            Title = AutoUpdate.Resources.Updating;
            btnCancel.Content = AutoUpdate.Resources.Cancel;
            btnRun.Content = AutoUpdate.Resources.Run;
            updator = new Updator();
            updator.MainWindow = this;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    updator.Update();
                }
                catch (Exception exc) { ShowError(exc); }
            }));
        }

        public void ShowError(Exception exc)
        {
            textBox.Text += exc.Message;
            btnCancel.IsEnabled = true;
            btnRun.IsEnabled = true;
        }

        public void AppendInfo(string text)
        {
            textBox.Text += text + "\r\n";
            textBox.ScrollToEnd();
            Application.Current.DoEvent();
        }

        public void UpdateProgress(double increment)
        {
            progressBar.Value += increment;
            Application.Current.DoEvent();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            updator.RunStartup();
            Close();
        }
    }
}
