using FastSockets.Networking;
using System.Collections.Generic;
using UnityEngine;

public class BV_Server : BaseServer<BV_Packets, BV_Server>
{
    public BV_Server() : base("", EConnectionType.SECTOR_SERVER, 1, 5)
    {
        string cfgString = "PORT=1234;NAME=Herpderp;";
        LoadConfigFromString(cfgString);
        SetPort();
    }

    protected bool ReceivedPacket_TestPacket(KeyValuePair<ClientConnection, object> testPkt)
    {
        Debug.Log("Test Packet received!");
        return true;
    }
}
