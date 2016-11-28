using FastSockets.Networking;
using System.Collections.Generic;
using UnityEngine;

public class BV_Client : BaseClient<BV_Packets, BV_Client>
{
    public BV_Client() : base("", 5)
    {

    }

    protected bool ReceivedPacket_TestPacket(KeyValuePair<ClientConnection, object> testPkt)
    {
        Debug.Log("Test Packet received!");
        return true;
    }
}
