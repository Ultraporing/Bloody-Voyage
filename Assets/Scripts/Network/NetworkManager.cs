using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Network
{
    public class NetworkManager : MonoBehaviour
    {
        public List<ClientWrapper> RemoteClientList;
        public ClientWrapper LocalClient = null;
        public ServerWrapper Server;

        void Start()
        {
            Server = gameObject.AddComponent<ServerWrapper>();
            LocalClient = gameObject.AddComponent<ClientWrapper>();
            LocalClient.Client.IsLocalClient = true;
        }

        public void Client_ConnectToLocalhost()
        {
            LocalClient.Client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234), 12345);
        }

        public void Client_Disconnect()
        {
            LocalClient.Client.Disconnect();
        }

        public void Server_Start()
        {
            Server.StartServer();
        }

        public void Server_Stop()
        {
            Server.ShutdownServer();
        }
    }
}
