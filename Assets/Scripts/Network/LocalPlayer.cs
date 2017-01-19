using FastSockets.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Controllers.Vehicles.Ship;

namespace Network
{
    public class PacketQueueContainer
    {
        public int PacketID;
        public object Packet;

        public PacketQueueContainer(int packetID, object packet)
        {
            PacketID = packetID;
            Packet = packet;
        }
    }

    [Serializable]
    public class LocalPlayer: BV_Client
    {
        public NetworkManager NetworkMgr = null;
        public LocalShipController LocalShipController = null;
        public Queue<PacketQueueContainer> NetworkCommandQueue = new Queue<PacketQueueContainer>();
        public int ServerPingMS = 0;

        public LocalPlayer(NetworkManager networkMgr)
        {
            NetworkMgr = networkMgr;

            OnClientConnectedReceived += Client_OnClientConnectedReceived;
            OnClientDisconnectedReceived += Client_OnClientDisconnectedReceived;
            OnConnectionSuccess += Client_OnConnectionSuccess;
            OnConnectionFailed += Client_OnConnectionFailed;
            OnSetClientIDReceived += Client_OnSetClientIDReceived;
            OnDisconnected += Client_OnDisconnected;

            OnPktReceivedSendClientList += Client_OnPktReceivedSendClientList;
            OnPktReceivedGameSyncTransform += Client_OnPktReceivedGameSyncTransform;
            OnPktReceivedGameSetSailingStage += Client_OnPktReceivedGameSetSailingStage;
        }

        #region API_Events
        private void Client_OnClientConnectedReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the Client Connected Packet. Sender: " + pkt.Key.ThisID + ".");

            NetworkCommandQueue.Enqueue(new PacketQueueContainer((int)EBasePackets.ClientConnected, pkt.Value));
            //NetworkMgr.Spawned = false;
        }

        private void Client_OnClientDisconnectedReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the Client Disconnected Packet. Sender: " + pkt.Key.ThisID + ".");

            NetworkCommandQueue.Enqueue(new PacketQueueContainer((int)EBasePackets.ClientDisconnected, pkt.Value));
        }

        private void Client_OnSetClientIDReceived(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Client has received the SetClientID Packet. Sender: " + pkt.Key.ThisID + ". Data: " + ((PacketDesc_SetClientID)pkt.Value).Id);
            PacketDesc_SendClientList pktSendList = new PacketDesc_SendClientList();
            pktSendList.PacketTarget = EConnectionType.SECTOR_SERVER;
            SendPacketToParent(pktSendList);
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
        #endregion

        #region Pkt Events
        private void Client_OnPktReceivedSendClientList(KeyValuePair<ClientConnection, object> pkt)
        {
            PacketDesc_SendClientList packet = ((PacketDesc_SendClientList)pkt.Value);

            Debug.Log("Client has received the SendClientList Packet. Sender: " + pkt.Key.ThisID + ". Data: " + packet.ClientIDs.ToString());

            NetworkCommandQueue.Enqueue(new PacketQueueContainer((int)BV_Packets.SendClientList, pkt.Value));

            //NetworkMgr.GotList = true;
        }
        
        private void Client_OnPktReceivedGameSyncTransform(KeyValuePair<ClientConnection, object> pkt)
        {
            PacketDesc_GameSyncTransform p = ((PacketDesc_GameSyncTransform)pkt.Value);

            NetworkCommandQueue.Enqueue(new PacketQueueContainer((int)BV_Packets.GameSyncTransform, p));
        }

        private void Client_OnPktReceivedGameSetSailingStage(KeyValuePair<ClientConnection, object> pkt)
        {
            NetworkCommandQueue.Enqueue(new PacketQueueContainer((int)BV_Packets.GameSetSailingStage, pkt.Value));
        }
        #endregion
    }
}
