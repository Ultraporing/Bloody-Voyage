using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FastSockets.Networking;

namespace Network
{
    public class ClientWrapper : MonoBehaviour
    {
        public BV_Client Client = new BV_Client();
        int n = 0;

        // Use this for initialization
        void Start()
        {
            Client.OnClientConnectedReceived += Client_OnClientConnectedReceived;
            Client.OnClientDisconnectedReceived += Client_OnClientDisconnectedReceived;
            Client.OnTestPacketReceived += Client_OnTestPacketReceived;
            Client.OnConnectionSuccess += Client_OnConnectionSuccess;
            Client.OnConnectionFailed += Client_OnConnectionFailed;
            Client.OnSetClientIDReceived += Client_OnSetClientIDReceived;
            Client.OnDisconnected += Client_OnDisconnected;
        }

        void OnDestroy()
        {
            Client.Shutdown();
        }

        // Update is called once per frame
        void Update()
        {
            Client.Update();

            if (Client.IsConnected())
            {
                if (n == 0)
                {
                    Debug.Log("Sending Packet");

                    PacketDesc_TestPacket tstPkt = new PacketDesc_TestPacket();
                    tstPkt.PacketTarget = EConnectionType.SECTOR_SERVER;
                    tstPkt.someTestVector = new Vector3(123, 55, 1);

                    Client.SendPacketToParent(tstPkt);

                    n = 1;
                }
            }
        }

        private void Client_OnTestPacketReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the Test Packet. Sender: " + pkt.Key.ThisID + ". Data: " + ((PacketDesc_TestPacket)pkt.Value).someTestVector);
        }

        private void Client_OnClientConnectedReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the Client Connected Packet. Sender: " + pkt.Key.ThisID + ".");
        }

        private void Client_OnClientDisconnectedReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the Client Disconnected Packet. Sender: " + pkt.Key.ThisID + ".");
        }

        private void Client_OnSetClientIDReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the SetClientID Packet. Sender: " + pkt.Key.ThisID + ". Data: " + ((PacketDesc_SetClientID)pkt.Value).Id);
        }

        private void Client_OnConnectionSuccess()
        {
            Debug.Log("Client has Successfully connected the Server.");
        }

        private void Client_OnConnectionFailed()
        {
            Debug.Log("Client has Failed to connect to the Server.");
        }

        private void Client_OnDisconnected()
        {
            Debug.Log("Client has Disconnected.");
        }
    }
}

