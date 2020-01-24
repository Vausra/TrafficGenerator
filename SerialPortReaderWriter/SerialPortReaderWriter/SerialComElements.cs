/// <summary>
/// Namespace of the serial port traffic generator
/// </summary>
namespace SerialPortReaderWriter
{
    /// <summary>
    /// Data container object.
    /// </summary>
    public class SerialComElements
    {
        /// <summary>
        /// Gets or sets the MessageName of a request/response.
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// Gets or sets the MessageResponse.
        /// </summary>
        public string MessageResponse { get; set; }

        /// <summary>
        /// Gets or sets the MessageRequest.
        /// </summary>
        public string MessageRequest { get; set; }
    }
}
