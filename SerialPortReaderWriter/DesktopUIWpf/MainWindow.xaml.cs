using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace DesktopUIWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SerialReader.SerialPortReaderWriter test;
        bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Stopbutton.IsEnabled = false;
        }

        
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Stopbutton.IsEnabled = true;
            Startbutton.IsEnabled = false;

            if(!isInitialized)
            {
                this.InitialzeComPort();
            }
            this.test.StartReadWriteData();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.test.StopWriteData();// = new SerialReader.SerialPortReaderWriter("COM4");
            Startbutton.IsEnabled = true;
        }

        private void InitialzeComPort()
        {
            this.test = new SerialReader.SerialPortReaderWriter(this.SerialConfig.PortName_TextBox.Text);
        }
    }
}
