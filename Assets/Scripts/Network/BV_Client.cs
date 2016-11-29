using FastSockets;
using FastSockets.Networking;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class BV_Client : BaseClient<BV_Packets, BV_Client>
    {
        public bool IsLocalClient = false;
        public event OnPacketReceivedCallback OnTestPacketReceived;

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

        protected bool ReceivedPacket_TestPacket(KeyValuePair<ClientConnection, object> testPkt)
        {
            if (OnTestPacketReceived != null)
            {
                OnTestPacketReceived(testPkt);
            }

            return true;
        } 
    }
}
