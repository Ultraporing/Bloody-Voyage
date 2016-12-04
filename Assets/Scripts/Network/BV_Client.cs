using Controllers.Vehicles.Ship;
using FastSockets;
using FastSockets.Networking;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    [Serializable]
    public class BV_Client : BaseClient<BV_Packets, BV_Client>
    {
        protected OnPacketReceivedCallback OnPktReceivedSendClientList, OnPktReceivedGameSyncTransform;

        public BV_Client() : base("", 5000)
        {
        }
                
        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Update()
        {
            // update here

            base.Update();
        }
        
        protected bool ReceivedPacket_SendClientList(KeyValuePair<ClientConnection, object> pkt)
        {
            if (OnPktReceivedSendClientList != null)
            {
                OnPktReceivedSendClientList(pkt);
            }

            return true;
        }

        protected bool ReceivedPacket_GameSyncTransform(KeyValuePair<ClientConnection, object> pkt)
        {
            if (OnPktReceivedGameSyncTransform != null)
            {
                OnPktReceivedGameSyncTransform(pkt);
            }

            return true;
        }
    }
}
