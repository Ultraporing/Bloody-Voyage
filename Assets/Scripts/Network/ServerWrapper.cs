using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FastSockets.Networking;

namespace Network
{
    public class ServerWrapper : MonoBehaviour
    {
        public BV_Server Server = new BV_Server();

        // Use this for initialization
        void Start()
        {
            Server.OnTestPacketReceived += Server_OnTestPacketReceived;
            Server.OnClientAccepted += Server_OnClientAccepted;
            Server.OnClientDisconnected += Server_OnClientDisconnected; 
        }

        public void StartServer()
        {
            Server.Start();
        }

        public void ShutdownServer()
        {
            Server.Shutdown();
        }

        void OnDestroy()
        {
            Server.Shutdown();
        }

        // Update is called once per frame
        void Update()
        {
            Server.Update();
        }

        private void Server_OnTestPacketReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Server has received the Test Packet. Sender: " + pkt.Key.ThisID + ". Data: " + ((PacketDesc_TestPacket)pkt.Value).someTestInt);
        }

        private void Server_OnClientAccepted(int clientID)
        {
            Debug.Log("Server has accepted a new Client with the ID: " + clientID);
        }

        private void Server_OnClientDisconnected(int clientID)
        {
            Debug.Log("Server has disconnected the Client with the ID: " + clientID);
        }
    }
}
