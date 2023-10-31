using LandC_Final_Project.Custom_Protocol.DataSerializerFactory.Interface;
using Newtonsoft.Json;
using LandC_Final_Project.Model;

namespace LandC_Final_Project.Custom_Protocol.DataSerializerFactory.DataSerializer
{
    public class JSONSerializer : IDataSerializer
    {
        public CommunicationProtocol DeSerialize(string data)
        {
            CommunicationProtocol protocol = JsonConvert.DeserializeObject<CommunicationProtocol>(data);
            return protocol;
        }

        public string Serialize(CommunicationProtocol protocol)
        {
            string json=JsonConvert.SerializeObject(protocol,Formatting.Indented);
            return json;
        }
    }
}
