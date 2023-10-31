using Newtonsoft.Json;
using System.Text;

namespace LandC_Final_Project.Custom_Protocol
{
    public class ISCRequest : CommunicationProtocol
    {
        public Dictionary<string, string> HeaderParameters = new Dictionary<string, string>();
        public string Method { get; set; }
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public string RequestCommand(string inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest))
            {
                throw new ArgumentException("Input request cannot be null or empty.");
            }
            string[] parsedInputCommand = inputRequest.Split(' ');
            for (int requestIndex = 1; requestIndex < parsedInputCommand.Length; requestIndex += 2)
            {
                if (parsedInputCommand[requestIndex] == "-a")
                {
                    Method = parsedInputCommand[requestIndex + 1];
                    HeaderParameters.Add("-a", Method);
                }
                if (parsedInputCommand[requestIndex] == "-i")
                {
                    InputFilePath = parsedInputCommand[requestIndex + 1];
                }
                if (parsedInputCommand[requestIndex] == "-o")
                {
                    OutputFilePath = parsedInputCommand[requestIndex + 1];
                }
            }
            return OutputFilePath;
        }
        public byte[] InputDataFromFile()
        {
            if (string.IsNullOrEmpty(InputFilePath))
            {
                throw new InvalidOperationException("Input file path cannot be null or empty.");
            }
            try
            {
                string inputDataFromFile = File.ReadAllText(InputFilePath);
                byte[] inputDataIntoBytes = Encoding.ASCII.GetBytes(inputDataFromFile);
                return inputDataIntoBytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while reading input file", ex);
            }
        }
        public string SendProtocolDetailToClient()
        {
            try
            {
                ISCRequest iscRequest = new ISCRequest();
                iscRequest.ProtocolType = iscRequest.Request;
                iscRequest.Size = InputDataFromFile().Length;
                iscRequest.Data = InputDataFromFile();
                iscRequest.ProtocolFormat = "json";
                iscRequest.Headers = HeaderParameters;
                iscRequest.SourceIp = "fe80::6fbb:ef49:cc2b:3a48%14";
                iscRequest.SourcePort = 11111;
                iscRequest.DestIp = "fe80::6fbb:ef49:cc2b:3a48%14";
                iscRequest.DestPort = 11111;
                string serializedData = JsonConvert.SerializeObject(iscRequest, Formatting.Indented);
                return serializedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while serializing into object", ex);
            }
        }
        public void ReceivedData(string receivedData, string outputFilePath)
        {
            try
            {
                File.WriteAllText(outputFilePath, receivedData);
                Console.WriteLine("Response recieved : " + receivedData);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while wirting data into file", ex);
            }
        }
    }
}
