using LandC_Final_Project.Model;
using System.Runtime.Serialization.Formatters.Binary;

namespace LandC_Final_Project.Custom_Protocol
{
    public class ISCResponse : CommunicationProtocol
    {
        public Dictionary<string, string> headerParameters { get; set; }
        private  string STATUS = "status";
        private  string ERROR_CODE = "error-code";
        private  string ERROR_MESSAGE = "error-message";

        public ISCResponse()
        {
            //this.ProtocolType = Response;
            //headerParameters.Add(STATUS, "");
            //headerParameters.Add(ERROR_CODE, "");
            //headerParameters.Add(ERROR_MESSAGE, "");
            //this.Headers = headerParameters;
        }
        public ISCResponse SetResponse(TeamList teamList)
        {
            ISCResponse iscResponse = new ISCResponse();
            iscResponse.ProtocolType = iscResponse.Response;
            iscResponse.Data=ObjectToByteArray(teamList);
            if (iscResponse.Data != null)
            {
                iscResponse.STATUS = "200";
                iscResponse.ERROR_MESSAGE = "";
            }
            return iscResponse;
        }

        //public string ErrorMessage
        //{
        //    get { return GetValue(ERROR_MESSAGE); }
        //    set { SetValue(ERROR_MESSAGE, value); }
        //}

        //public string GetValue(string key)
        //{
        //    return Headers[key];
        //}

        //public void SetValue(string key, string value)
        //{
        //    headerParameters[key] = value;
        //    Headers = headerParameters;
        //}
        private static byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
    }
}
