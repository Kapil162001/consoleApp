using System.Net;

namespace LandC_Final_Project.Custom_Protocol
{
    public class ISCRequest : CommunicationProtocol
    {
        public Dictionary<string, string> headerParameters = new Dictionary<string, string>();
        public string Method = "create_team";

        //public ISCRequest(int port, IPAddress IPAddress, int size, byte[] data)
        //{
        //    this.Size = size;
        //    this.Data = data;
        //    this.ProtocolType = Request;
        //    this.SourcePort = port;
        //    this.SourceIp = IPAddress.ToString();
        //    this.DestinationPort = port;
        //    this.DestinationIp = IPAddress.ToString();
        //    this.ProtocolFormat = "json";
        //    this.headerParameters.Add("method", Method);
        //}

    }
}
