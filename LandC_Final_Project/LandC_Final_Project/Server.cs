using LandC_Final_Project.Custom_Protocol;
using LandC_Final_Project.Custom_Protocol.DataSerializerFactory;
using LandC_Final_Project.Custom_Protocol.DataSerializerFactory.Interface;
using LandC_Final_Project.Model;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Server
{
    class Server
    {
        private static readonly int _portNumber = 11111;
        private static readonly string _hostName = Dns.GetHostName();
        private static readonly IPHostEntry _ipHost = Dns.GetHostEntry(_hostName);
        private static readonly IPAddress _ipAddress = _ipHost.AddressList[0];
        private static readonly IPEndPoint _localEndPoint = new IPEndPoint(_ipAddress, _portNumber);
        private static readonly Socket _serverSocket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> _clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 5120;

        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        private readonly TeamController _teamController;
        public Server()
        {
            _teamController = new TeamController();
        }
        public static void ConnectToServer()
        {
            try
            {
                Console.Title = "Server";
                SetupServer();
                Console.WriteLine("Press any key to close the server");
                Console.ReadKey();
                CloseAllSockets();
            }
            catch (SocketException ex)
            {
                throw new Exception($"Error while connecting to the server:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpexted error while connecting to the server:{ex.Message}");
            }
        }
        private static void SetupServer()
        {
            try
            {
                Console.WriteLine("Setting up server...");
                _serverSocket.Bind(_localEndPoint);
                _serverSocket.Listen(10);
                _serverSocket.BeginAccept(AcceptCallback, null);
                Console.WriteLine("Server setup complete");
            }
            catch (SocketException ex)
            {
                throw new Exception($"Error while setup the server:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpexted error while setup the server:{ex.Message}");
            }
        }
        private static void CloseAllSockets()
        {
            try
            {
                foreach (Socket socket in _clientSockets)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                _serverSocket.Close();
            }
            catch (SocketException ex)
            {
                throw new Exception($"Error while closing:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpexted error closing:{ex.Message}");
            }
        }
        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;
            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            _clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            _serverSocket.BeginAccept(AcceptCallback, null);
        }
        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            Server server = new Server();
            ISCRequest iscRequest = new ISCRequest();
            ISCResponse iscResponse = new ISCResponse();
            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                current.Close();
                _clientSockets.Remove(current);
                return;
            }
            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);

            Console.WriteLine("Received Text: " + text);
            Console.WriteLine("Client request is " + text);
            if (text == "exit")
            {
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                _clientSockets.Remove(current);
                Console.WriteLine("Client disconnected");
                return;
            }
            iscRequest = JsonConvert.DeserializeObject<ISCRequest>(text);
            string protocolFormat = iscRequest.ProtocolFormat;
            IDataSerializer dataSerializer = DataSerializerFactory.getSerializer(protocolFormat);

            CommunicationProtocol protocolObject = dataSerializer.DeSerialize(text);

            string key = protocolObject.Headers.Keys.First();
            string methodName = protocolObject.Headers[key];
            if (methodName == "create_team")
            {
                string convertedData = Encoding.ASCII.GetString(protocolObject.Data);
                try
                {
                    Game game = JsonConvert.DeserializeObject<Game>(convertedData);
                    TeamList teamList = server._teamController.TeamCreation(game);
                    string jsonData = JsonConvert.SerializeObject(teamList, Formatting.Indented);
                    byte[] data = GetBytes(jsonData);
                    SendDataToCleint(data, current);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex.Message);
                    byte[] data = GetBytes(ex.Message);
                    SendDataToCleint(data, current);
                }
            }
        }
        private static void SendDataToCleint(byte[] data, Socket currentSocket)
        {
            try
            {
                currentSocket.Send(data);
                Console.WriteLine("Response sent to client");
                currentSocket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, currentSocket);
            }
            catch (SocketException ex)
            {
                throw new Exception($"Error while sending data client:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpexted error while sending data to client:{ex.Message}");
            }
        }
        private static byte[] GetBytes(string text)
        {
            try
            {
                return Encoding.ASCII.GetBytes(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while converting given string:{ex.Message}");
            }
        }
    }
}
