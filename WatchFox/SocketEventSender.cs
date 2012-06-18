using System;
using System.Text;
using Zeta.Common;
using Zeta.CommonBot;
using System.Net;
using System.Net.Sockets;

namespace Kbits.Demonbuddy.Plugins
{
    class SocketEventSender
    {
        TcpClient _tcpClient = new TcpClient();
        NetworkStream _serverStream = default(NetworkStream);

        private string _serverIP = "127.0.0.1";
        private int _serverPort = 9050;

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

        public void Enable()
        {
            try
            {
                _tcpClient.Connect(_serverIP, _serverPort);
                _serverStream = _tcpClient.GetStream();
            }
            catch (SocketException e)
            {
                Logging.Write(String.Format("[Watchfox] error connecting to {0}:{1}", _serverIP, _serverPort));
            }
        }

        public void Disable()
        {
            try
            {
                _tcpClient.Close();
            }
            catch (Exception e)
            {
                Logging.Write(String.Format("[Watchfox] error closing socket: {0}", e));
            }
        }

        public void Looted(ItemLootedEventArgs itemLootedEventArgs)
        {
            SendStringToSocket("{\"event\": \"loot\", \"msg\": \"Watchfox looted " + itemLootedEventArgs.Item.Name + ", " + itemLootedEventArgs.Item.ItemBaseType + "\"}");
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

        public void GameJoined()
        {
            SendStringToSocket("{\"event\": \"joined\", \"msg\": \"Watchfox game joined.\"}");
        }

        public void GameLeft(WatchFoxStats stats)
        {
            var json = "{\"event\": \"left\", \"stats\": "+ stats.AsJsonStyledString() +"}";

            SendStringToSocket(json);
        }
    }
}