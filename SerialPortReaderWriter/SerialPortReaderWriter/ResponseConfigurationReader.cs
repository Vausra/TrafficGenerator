namespace SerialPortReaderWriter
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    /// <summary>
    /// Data container for responses on a specific request.
    /// </summary>
    public class ResponseConfigurationReader
    {
        private const string RootElement = "Queries";
        private const string MessageName = "MessageName";

        private const string MessageRequest = "Request";

        private const string MessageResponse = "Response";

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseConfigurationReader"/> class.
        /// </summary>
        public ResponseConfigurationReader()
        {

        }

        /// <summary>
        /// Try to read given file ad creat a list of datacontainer for requests and responses
        /// </summary>
        /// <param name="fileToRead">String with path and file name</param>
        /// <returns><see cref="List{T}"/> of <see cref="SerialComElements"/> parsed from given json</returns>
        public List<SerialComElements> ReadConfigFile(string fileToRead)
        {
            List<SerialComElements> command = new List<SerialComElements>();

            var jsonBytes = File.ReadAllBytes(fileToRead);

            JsonDocument jsonDoc = JsonDocument.Parse(jsonBytes);
            JsonElement myString = jsonDoc.RootElement.GetProperty(RootElement);

            foreach (JsonElement elem in myString.EnumerateArray())
            {
                // var elem = jsonProperty.Value;
                string messageName = elem.GetProperty(MessageName).GetString();
                SerialComElements tmpObj = new SerialComElements
                {
                    MessageName = elem.GetProperty(MessageName).ToString(),
                    MessageRequest = this.HexPropertyListToString(elem.GetProperty(MessageRequest), MessageRequest),
                    MessageResponse = this.HexPropertyListToString(elem.GetProperty(MessageResponse), MessageResponse),
                };

                command.Add(tmpObj);
            }

            return command;
        }

        private string HexPropertyListToString(JsonElement jsonListElemnt, string jsonPropery)
        {
            string asciiString = string.Empty;
            foreach (var arrayElem in jsonListElemnt.EnumerateArray())
            {
                byte decval = Convert.ToByte(arrayElem.ToString().ToLower(), 16);
                asciiString += System.Convert.ToChar(decval);
            }

            return asciiString;
        }
    }
}
