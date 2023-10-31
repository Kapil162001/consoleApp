using LandC_Final_Project.Custom_Protocol.DataSerializerFactory.DataSerializer;
using LandC_Final_Project.Custom_Protocol.DataSerializerFactory.Interface;

namespace LandC_Final_Project.Custom_Protocol.DataSerializerFactory
{
    public class DataSerializerFactory
    {
        public static string JSON = "json";
        public static string XML = "xml";
        public IDataSerializer DataSerializer;
        public static IDataSerializer getSerializer(string protocolFormat)
        {
            if (protocolFormat.Equals(JSON))
            {
                return new JSONSerializer();
            }
            else if (protocolFormat.Equals(XML))
            {
                return new XMLSerializer();
            }
            else
            {
                return null;
            }
        }
    }
}
