// <copyright file="SerialPortReaderWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SerialCommInterface
{
    using System;
    using System.IO.Ports;
    using System.Threading;

    /// <summary>
    /// Allowed Bytesize for the serial port configuration.
    /// </summary>
    public enum Bytesize
    {
        /// <summary> Five byte size for data bits. </summary>
        Five_Byte = 5,

        /// <summary>Six byte size for data bits. </summary>
        Six_Byte = 6,

        /// <summary> Seven byte size for data bits. </summary>
        Seven_Byte = 7,

        /// <summary>Eight byte size for data bits.</summary>
        Eight_Byte = 8,

        /// <summary>Nine byte size for data bits.</summary>
        Nine_Byte = 9,
    }

    /// <summary> Allowed Baudrates.</summary>
    public enum Baudrate
    {
        /// <summary>Baudrate at 9600.</summary>
        Baudrate_9600 = 9600,
    }

    /// <summary>
    /// Instance of the SerialPortReaderWriter.
    /// </summary>
    public class SerialPortReaderWriter : IDisposable
    {

        private readonly SerialPort serialPort;
        private Thread readWriteThread;

        private bool runEnabled = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortReaderWriter"/> class.
        /// </summary>
        /// <param name="comPort">Name of the comport to open.</param>
        public SerialPortReaderWriter(string comPort)
            : this(comPort, Baudrate.Baudrate_9600)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortReaderWriter"/> class.
        /// </summary>
        /// <param name="comPort">Name of the comport to open.</param>
        /// <param name="baudrate">Baudrate of the serial connection</param>
        public SerialPortReaderWriter(string comPort, Baudrate baudrate)
        : this(comPort, baudrate, Parity.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortReaderWriter"/> class.
        /// </summary>
        /// <param name="comPort">Name of the comport to open.</param>
        /// <param name="baudrate">Baudrate of the serial connection.</param>
        /// <param name="parity">Parity bit fo the serial connection.</param>
        public SerialPortReaderWriter(string comPort, Baudrate baudrate, Parity parity)
            : this(comPort, baudrate, parity, Bytesize.Eight_Byte)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortReaderWriter"/> class.
        /// </summary>
        /// <param name="comPort">Name of the comport to open.</param>
        /// <param name="baudrate">Baudrate of the serial connection</param>
        /// <param name="parity">Parity bit fo the serial connection.</param>
        /// <param name="byteSize">Size of the data part in </param>
        public SerialPortReaderWriter(string comPort, Baudrate baudrate, Parity parity, Bytesize byteSize)
            : this(comPort, baudrate, parity, byteSize, StopBits.One)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortReaderWriter"/> class.
        /// </summary>
        /// <param name="comPort">Name of the comport to open.</param>
        /// <param name="baudrate">Baudrate of the serial connection</param>
        /// <param name="parity">Parity bit fo the serial connection.</param>
        /// <param name="bytesize">Size of the data part in </param>
        /// <param name="stopbits">Number of used stop bits</param>
        public SerialPortReaderWriter(string comPort, Baudrate baudrate, Parity parity, Bytesize bytesize, StopBits stopbits)
        {
            this.serialPort = new SerialPort(comPort, (int)baudrate, parity, (int)bytesize, stopbits);
            this.serialPort.Open();
            this.serialPort.DataReceived += this.SerialPort_DataReceived;
        }

        /// <summary>
        /// Start thread to read and write data on comport.
        /// </summary>
        /// <param name="data">data to write.</param>
        public void StartReadWriteData()
        {
            this.readWriteThread = new Thread(this.WriteDataToPort);
            this.readWriteThread.Start();
        }

        /// <summary>
        /// Stop the thread for reading and writing on com port.
        /// </summary>
        public void StopWriteData()
        {
            this.runEnabled = false;
        }

        /// <summary>
        /// Get a list of available com ports on the system.
        /// </summary>
        /// <returns><see cref="string[]"/> of comports.</returns>
        public static string[] ListComPorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// Dispose the serial port object.
        /// </summary>
        public void Dispose()
        {
            if (this.serialPort != null)
            {
                this.runEnabled = false;
                this.readWriteThread.Join();
                this.serialPort.Dispose();
            }
        }

        private void WriteDataToPort()
        {
            this.runEnabled = true;
            while (this.runEnabled)
            {
                Thread.Sleep(1000);

                // Console.Write("No Data");
                this.WriteData();
            }
        }

        /// <summary>
        /// Event for incoming data on serial port.
        /// </summary>
        /// <param name="s">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs eventArgs)
        {
            string result = this.serialPort.ReadExisting();

            Console.WriteLine(result);
        }

        private void WriteData()
        {
            // only write data if present in buffer.
            this.serialPort.Write("Test it.");
        }
    }
}