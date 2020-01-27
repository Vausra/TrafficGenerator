using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for IpAdressAndPortInput.xaml
    /// </summary>
    public partial class IpAdressAndPortInput : UserControl
    {
        public IpAdressAndPortInput()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ClearIpAddressButton_Click(object sender, RoutedEventArgs e)
        {
            PortIpAddress.Text = string.Empty;
            FourthByteIpAddress.Text = string.Empty;
            ThirdByteIpAddress.Text = string.Empty;
            SecondByteIpAddress.Text = string.Empty;
            FirsByteIpAddress.Text = string.Empty;
            FirsByteIpAddress.Text = string.Empty;
        }
    }
}
