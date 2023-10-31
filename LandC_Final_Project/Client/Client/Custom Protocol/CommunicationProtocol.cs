using System.Net;

namespace LandC_Final_Project.Custom_Protocol
{
    public class CommunicationProtocol
    {
        public int Size { get; set; }
        public string Request = "request";
        public string Response = "response";
        public byte[] Data { get; set; }
        public string ProtocolVersion { get; set; }
        public string ProtocolFormat { get; set; }
        public string ProtocolType { get; set; }
        public string SourceIp { get; set; }
        public int SourcePort { get; set; }
        public string DestIp { get; set; }
        public int DestPort { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
