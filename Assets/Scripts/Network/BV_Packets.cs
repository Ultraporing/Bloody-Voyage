using Assets.Scripts.Network.Types;
using FastSockets.Networking;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public enum BV_Packets
    {
        SendClientList = EBasePackets.NUM_PACKETS,
        LobbyReady,
        GameFinishedLoading,
        GameSyncTransform,
        GameFireOneOnSide,
        GameFireAllOnSide,
        GameSetSailingStage,
        
        NUM_PACKETS
    }

    [Serializable]
    public class PacketDesc_SendClientList : BasePacket<BV_Packets>
    {
        public List<int> ClientIDs = new List<int>();
    }

    [Serializable]
    public class PacketDesc_GameSetSailingStage : BasePacket<BV_Packets>
    {
        public int OtherID;
        public int SailingStage;
    }

    [Serializable]
    public class PacketDesc_LobbyReady : BasePacket<BV_Packets>
    {
        public int OtherID;
    }

    [Serializable]
    public class PacketDesc_GameFinishedLoading : BasePacket<BV_Packets>
    {
        public int OtherID;
    }

    [Serializable]
    public class PacketDesc_GameSyncTransform : BasePacket<BV_Packets>
    {
        public int OtherID;
        public int OtherPing;
        public BV_Vector3 WorldPosition, RotationEuler, LocalScale;
    }

    [Serializable]
    public class PacketDesc_GameFireOneOnSide : BasePacket<BV_Packets>
    {
        public int OtherID;
        public int Side, Position;
    }

    [Serializable]
    public class PacketDesc_GameFireAllOnSide : BasePacket<BV_Packets>
    {
        public int OtherID;
        public int Side;
    }
}
