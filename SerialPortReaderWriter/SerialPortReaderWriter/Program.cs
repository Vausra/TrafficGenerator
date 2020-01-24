namespace SerialReader
{

    using System;
    using System.IO.Ports;
    using System.Text.Json;
    using System.Threading;
    
    class Program
    {
        static void Main(string[] args)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var reader = new SerialPortTest("COM4");

            while (true)
            {
                Thread.Sleep(1000);
                // Console.Write("No Data");
                reader.WriteData("There should be data on com3");

            }

        }

        static void ListComPorts()
        {
            // Get a list of serial port names. 
            string[] ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console. 
            foreach (string port in ports)
            {
                Console.WriteLine(port);
            }
        }
    }

    public class SerialPortTest : IDisposable
    {
        private SerialPort _serialPort;

        public SerialPortTest(string portName)
        {
            _serialPort = new SerialPort(portName);
            _serialPort.Open();
            _serialPort.DataReceived += serialPort_DataReceived;
        }

        void serialPort_DataReceived(object s, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(_serialPort.ReadExisting());
        }

        public void Dispose()
        {
            if (_serialPort != null)
            {
                _serialPort.Dispose();
            }
        }

        public void WriteData(string dataToWrite)
        {
            _serialPort.Write(dataToWrite);
        }
    }
}