using FastSockets.Networking;
using System;

public enum BV_Packets
{
    TestPacket = FastSockets.Networking.EBasePackets.NUM_PACKETS
}

[Serializable]
public class PacketDesc_TestPacket : BasePacket<BV_Packets>
{
    public int someTestInt;
}
