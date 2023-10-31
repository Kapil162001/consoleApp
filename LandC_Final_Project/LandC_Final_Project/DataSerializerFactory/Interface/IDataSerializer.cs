namespace LandC_Final_Project.Custom_Protocol.DataSerializerFactory.Interface
{
    public interface IDataSerializer
    {
        //private readonly string REQUEST = "request";

        //private readonly string RESPONSE = "response";

        CommunicationProtocol DeSerialize(string data);
        string Serialize(CommunicationProtocol protocol);
    }
}
