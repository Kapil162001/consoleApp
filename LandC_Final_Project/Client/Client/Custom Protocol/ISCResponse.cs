namespace LandC_Final_Project.Custom_Protocol
{
    public class ISCResponse : CommunicationProtocol
    {
        private Dictionary<string, string> headerParameters = new Dictionary<string, string>();
        private const string STATUS = "status";
        private const string ERROR_CODE = "error-code";
        private const string ERROR_MESSAGE = "error-message";
        public ISCResponse()
        {
            this.ProtocolType = Response;
            headerParameters.Add(STATUS, "");
            headerParameters.Add(ERROR_CODE, "");
            headerParameters.Add(ERROR_MESSAGE, "");
            this.Headers = headerParameters;
        }
        public string ErrorMessage
        {
            get { return GetValue(ERROR_MESSAGE); }
            set { SetValue(ERROR_MESSAGE, value); }
        }
        public string GetValue(string key)
        {
            return Headers[key];
        }
        public void SetValue(string key, string value)
        {
            headerParameters[key] = value;
            Headers = headerParameters;
        }
    }
}
