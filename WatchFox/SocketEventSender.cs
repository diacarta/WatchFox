using System;
using System.Text;
using Zeta.Common;
using Zeta.CommonBot;
using System.Net;
using System.Net.Sockets;

namespace Kbits.Demonbuddy.Plugins
{
    class SocketEventSender : IDbEventHandler
    {
        TcpClient _tcpClient = new TcpClient();
        NetworkStream _serverStream = default(NetworkStream);

        public SocketEventSender()
        {
            try
            {
                _tcpClient.Connect("127.0.0.1", 9050);
                _serverStream = _tcpClient.GetStream();
            }
            catch (SocketException e)
            {
                Logging.Write("[Watchfox] error connecting to endpoint: " + e);
            }
        }

        public void Error(string errorMessage)
        {
            SendStringToSocket(errorMessage);
        }

        public void Dispose()
        {
        }

        public void LevelUp(string name, int level)
        {
        }

        public void Start(string message)
        {
            SendStringToSocket(message);
        }

        public void Stop(string message)
        {
            SendStringToSocket(message);
        }

        public void Looted(ItemLootedEventArgs itemLootedEventArgs)
        {
        }

        public void ShutDown()
        {
        }

        void SendStringToSocket(string message)
        {
            byte[] outStream = Encoding.ASCII.GetBytes(message);
            _serverStream.Write(outStream, 0, outStream.Length);
            _serverStream.Flush();
        }
    }
}