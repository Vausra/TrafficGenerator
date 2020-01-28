using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SerialCommInterface;

namespace DesktopUIWpf
{
    /// <summary>
    /// Interaction logic for ComPortList.xaml
    /// </summary>
    public partial class ComPortList : Window
    {

        Action<string> setPortNameCallback;
        private SerialPortReaderWriter serialPort;
        public ComPortList(SerialPortReaderWriter serialPort, Action<string> callback)
        {
            InitializeComponent();

            this.setPortNameCallback = callback;
            this.serialPort = serialPort;
        }

        private void ListPortsButton_Click(object sender, RoutedEventArgs e)
        {
            string[] result = SerialPortReaderWriter.ListComPorts();
            this.ComPorts_ListBox.ItemsSource = result;
        }

        private void ComPorts_ListBox_ItemDoubleClick(object sender, System.EventArgs e)
        {
            this.setPortNameCallback(ComPorts_ListBox.SelectedItem.ToString());
            this.Close();
        }
    }
}
