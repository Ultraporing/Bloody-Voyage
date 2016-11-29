using FastSockets.Networking;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class BV_Server : BaseServer<BV_Packets, BV_Server>
    {
        public event OnPacketReceivedCallback OnTestPacketReceived;

        public BV_Server() : base("", EConnectionType.SECTOR_SERVER, 1000, 5000)
        {
            string cfgString = "PORT=1234;NAME=Herpderp;";
            LoadConfigFromString(cfgString);
            SetPort();
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
