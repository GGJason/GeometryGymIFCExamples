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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using GeometryGym.Ifc;
using System.IO;

namespace WPFConvertToIFCJson
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string readPath;
        string writePath;

        private void fromButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.ifc|*.ifc";
            ofd.ShowDialog();
            fromLabel.Content = readPath = ofd.FileName;
            
        }

        private void toButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".json";
            sfd.Filter = "*.json|*.json";
            sfd.ShowDialog();
            toLabel.Content= writePath = sfd.FileName;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "Converting";
            DatabaseIfc db = new DatabaseIfc(readPath);
            db.ToJSON(writePath);
            statusLabel.Content = "Complete";
        }
    }
}
