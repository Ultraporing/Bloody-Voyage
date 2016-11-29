using FastSockets.Networking;
using System;

namespace Network
{
    public enum BV_Packets
    {
        TestPacket = EBasePackets.NUM_PACKETS,
        NUM_PACKETS
    }

    [Serializable]
    public class PacketDesc_TestPacket : BasePacket<BV_Packets>
    {
        public int someTestInt;
    }
}
