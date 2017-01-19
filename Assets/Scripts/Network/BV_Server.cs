using FastSockets.Networking;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    [Serializable]
    public class BV_Server : BaseServer<BV_Packets, BV_Server>
    {
        public BV_Server() : base("", EConnectionType.SECTOR_SERVER, 1000, 5000)
        {
            string cfgString = "PORT=1234;NAME=Herpderp;";
            LoadConfigFromString(cfgString);
            SetPort();

            OnClientAccepted += Server_OnClientAccepted;
            OnClientDisconnected += Server_OnClientDisconnected;
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Update()
        {
            if (IsRunning)
            {
                base.Update();
            }
        }

        protected bool ReceivedPacket_SendClientList(KeyValuePair<ClientConnection, object> pkt)
        {
            Debug.Log("Server has received the SendClientList Packet. Sender: " + pkt.Key.ThisID);

            PacketDesc_SendClientList pktSendList = new PacketDesc_SendClientList();
            pktSendList.PacketTarget = EConnectionType.CLIENT;

            pktSendList.ClientIDs.AddRange(Clients.Keys);

            SendPacketToClient(pktSendList, pkt.Key.ThisID);

            return true;
        }

        protected bool ReceivedPacket_GameSyncTransform(KeyValuePair<ClientConnection, object> pkt)
        {
            //Debug.Log("Server has received the SendClientList Packet. Sender: " + pkt.Key.ThisID);
            
            foreach (KeyValuePair<int, ClientConnection> cc in Clients)
            {
                if (cc.Key != pkt.Key.ThisID)
                {
                    PacketDesc_GameSyncTransform p = (PacketDesc_GameSyncTransform)pkt.Value;
                    p.PacketTarget = EConnectionType.CLIENT;
                    p.OtherPing = Clients[pkt.Key.ThisID].Ping + Clients[cc.Key].Ping;
                    SendPacketToClient(p, cc.Key);
                }
            }
            
            //SendPacketToAllClients(p, p.OtherID);

            return true;
        }

        protected bool ReceivedPacket_GameSetSailingStage(KeyValuePair<ClientConnection, object> pkt)
        {
            //Debug.Log("Server has received the SendClientList Packet. Sender: " + pkt.Key.ThisID);

            PacketDesc_GameSetSailingStage p = (PacketDesc_GameSetSailingStage)pkt.Value;
            p.PacketTarget = EConnectionType.CLIENT;

            SendPacketToAllClients(p, p.OtherID);

            return true;
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
