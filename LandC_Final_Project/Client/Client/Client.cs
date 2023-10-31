using LandC_Final_Project.Custom_Protocol;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client
{
    public class Client
    {
        private static readonly int _portNumber = 11111;
        private static readonly string _hostName = Dns.GetHostName();
        private static string _outputFilePath = string.Empty;
        private static readonly IPHostEntry _ipHost = Dns.GetHostEntry(_hostName);
        private static readonly IPAddress _ipAddress = _ipHost.AddressList[0];
        private static readonly IPEndPoint _localEndPoint = new IPEndPoint(_ipAddress, _portNumber);
        private static readonly Socket _clientSocket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        private readonly ISCRequest _iscRequest;
        public Client()
        {
            _iscRequest = new ISCRequest();
        }
        public static void Connect()
        {
            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
            Exit();
        }
        private static void ConnectToServer()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine($"Connection attempt {attempts}");
                    _clientSocket.Connect(_localEndPoint);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Error while connecting socket: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error while connecting: {ex.Message}");
                }
            }
            Console.Clear();
            Console.WriteLine("Connected");
        }
        private static void RequestLoop()
        {
            while (true)
            {
                SendRequest();
                ReceivedResponseFromServer();
            }
        }
        private static void Exit()
        {
            try
            {
                SendDataToServer("exit");
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error while closing socket: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while closing: {ex.Message}");
            }
            finally
            {
                Environment.Exit(0);
            }
        }
        private static void SendRequest()
        {
            Client client = new Client();
            Console.WriteLine("Enter the request");
            string request = Console.ReadLine();
            if (request == "exit")
            {
                Exit();
            }
            _outputFilePath = client._iscRequest.RequestCommand(request);
            string protocolData = client._iscRequest.SendProtocolDetailToClient();
            SendDataToServer(protocolData);
        }
        private static void SendDataToServer(string data)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                _clientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error while sending data to server: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while sending data: {ex.Message}");
            }
        }
        private static void ReceivedResponseFromServer()
        {
            try
            {
                Client client = new Client();
                var buffer = new byte[5120];
                int received = _clientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string receiveTextFromServer = Encoding.ASCII.GetString(data);
                client._iscRequest.ReceivedData(receiveTextFromServer, _outputFilePath);

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error while receiving data from server: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while receiving data: {ex.Message}");
            }
        }
    }
}
