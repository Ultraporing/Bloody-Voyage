using Assets.Scripts.Network.Types;
using FastSockets.Networking;
using System;
using UnityEngine;

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
        public BV_Vector3 someTestVector;
    }
}
