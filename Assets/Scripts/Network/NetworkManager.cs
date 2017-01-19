using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using Controllers.Vehicles.Ship;
using Network.Controllers.Vehicles.Ship;
using FastSockets.Networking;
using UI;
using FastSockets;

namespace Network
{
    [Serializable]
    public class RemotePlayers
    {
        public RemotePlayers(int ID)
        {
            ClientID = ID;
            RemotePlayer = new RemotePlayer();
        }

        public int ClientID;
        public RemotePlayer RemotePlayer;
    }

    [Serializable]
    public class NetworkManager : MonoBehaviour
    {
        bool IsServer = true;

        public List<RemotePlayers> RemotePlayers = new List<RemotePlayers>();
        public BV_Server Server = null;
        public LocalPlayer LocalPlayer = null;
        public List<Transform> SpawnPoints = new List<Transform>();
        [SerializeField]
        public int NumPlayers { get { return RemotePlayers.Count + 1; } }

        public GameObject LocalPlayerPrefab, RemotePlayerPrefab;
        //public bool GotList = false, Spawned = false;
        public int PacketListSize = 0;

        string ConnectIP = "86.49.25.193";

        void Start()
        {
            Application.targetFrameRate = 200;
            IsServer = false;//Application.isEditor;

            if (!IsServer)
            {
                string[] cmd = Environment.GetCommandLineArgs();

                foreach (string s in cmd)
                {
                    Debug.Log(s);
                    if (s.Contains("IP="))
                    {
                        string[] sp = s.Split('=');
                        ConnectIP = sp[1];
                    }
                }
            }

            Server = new BV_Server();
            LocalPlayer = new LocalPlayer(this);

            if (IsServer)
            {
                
                Server.Start(IPAddress.Any);
            }

            Client_ConnectToLocalhost();
        }

        private void Update()
        {
            if (PacketListSize != LocalPlayer.NetworkCommandQueue.Count)
                PacketListSize = LocalPlayer.NetworkCommandQueue.Count;

            while (LocalPlayer.NetworkCommandQueue.Count > 0)
            {
                HandleQueuePacket(LocalPlayer.NetworkCommandQueue.Dequeue());
            }
        }

        private void HandleQueuePacket(PacketQueueContainer pqc)
        {
            switch (pqc.PacketID)
            {
                case (int)EBasePackets.ClientConnected:
                    AddRemotePlayer(((PacketDesc_ClientConnected)pqc.Packet).OtherClientID);
                    SpawnPlayer(((PacketDesc_ClientConnected)pqc.Packet).OtherClientID, 1);
                    break;
                case (int)EBasePackets.ClientDisconnected:
                    RemoveRemotePlayer(((PacketDesc_ClientDisconnected)pqc.Packet).OtherClientID);
                    break;
                case (int)EBasePackets.Ping:
                    break;
                case (int)EBasePackets.SetClientID:
                    break;
                case (int)BV_Packets.SendClientList:

                    SpawnPlayer(LocalPlayer.UniqueID, 0);
                    LocalPlayer.LocalShipController.OwnerClientID = LocalPlayer.UniqueID;
                    GameObject.Find("Canvas").GetComponent<UIManager>().Setup();

                    foreach (int i in ((PacketDesc_SendClientList)pqc.Packet).ClientIDs)
                    {
                        if (LocalPlayer.UniqueID != i)
                        {
                            if (FindRemotePlayerWithID(i) == null)
                            {
                                AddRemotePlayer(i);
                                SpawnPlayer(i, 1);
                            }
                        }
                    }

                    break;
                case (int)BV_Packets.LobbyReady:
                    break;
                case (int)BV_Packets.GameFinishedLoading:
                    break;
                case (int)BV_Packets.GameSyncTransform:
                    {
                        PacketDesc_GameSyncTransform pkt = (PacketDesc_GameSyncTransform)pqc.Packet;
                        RemotePlayers rm = FindRemotePlayerWithID(pkt.OtherID);
                        if (rm != null)
                        {
                            rm.RemotePlayer.RemoteShipController.Network_SetTransform(pkt.WorldPosition, pkt.RotationEuler, pkt.LocalScale, pkt.OtherPing);
                        }
                    }
                    break;
                case (int)BV_Packets.GameFireOneOnSide:
                    break;
                case (int)BV_Packets.GameFireAllOnSide:
                    break;
                case (int)BV_Packets.GameSetSailingStage:
                    {
                        PacketDesc_GameSetSailingStage pkt = (PacketDesc_GameSetSailingStage)pqc.Packet;
                        RemotePlayers rm = FindRemotePlayerWithID(pkt.OtherID);
                        if (rm != null)
                        {
                            rm.RemotePlayer.RemoteShipController.Network_SetTargetStage(pkt.SailingStage);
                        }
                    }
                    break;
            }
        }

        private void OnApplicationQuit()
        {
            if (LocalPlayer.IsConnected())
            {
                Client_Disconnect();
            }

            if (Server.IsRunning)
            {
                Server_Stop();
            }
        }

        public RemotePlayers FindRemotePlayerWithID(int id)
        {
            foreach (RemotePlayers rm in RemotePlayers)
            {
                if (rm.ClientID == id)
                {
                    return rm;
                }
            }

            return null;
        }

        public void AddRemotePlayer(int id)
        {
            RemotePlayers.Add(new RemotePlayers(id));
        }

        public void RemoveRemotePlayer(int id)
        {
            RemotePlayers.Remove(RemotePlayers.Find(x => x.ClientID == id));
        }

        public void Client_ConnectToLocalhost()
        {
            if (IsServer)
            {
                LocalPlayer.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234), new IPEndPoint(IPAddress.Loopback, 12345));
            }
            else
            {
                //LocalPlayer.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234), new IPEndPoint(IPAddress.Any, 12346));
                LocalPlayer.Connect(new IPEndPoint(IPAddress.Parse(ConnectIP), 1234), new IPEndPoint(IPAddress.Any, 12346));
            }
        }

        public void Client_Disconnect()
        {
            LocalPlayer.Disconnect();
        }

        public void Server_Stop()
        {
            Server.Shutdown();
        }

        public void SpawnPlayer(int id, int spawnPointIndex)
        {
            SpawnPlayer(id, SpawnPoints[spawnPointIndex].transform.position);
        }

        public void SpawnPlayer(int id, Vector3 spawnPos)
        {
            if (LocalPlayer.UniqueID == id)
            {
                LocalPlayer.LocalShipController = Instantiate(LocalPlayerPrefab, spawnPos, Quaternion.identity).GetComponent<LocalShipController>();
                return;
            }

            RemotePlayers rm = FindRemotePlayerWithID(id);
            if (rm != null)
            {
                rm.RemotePlayer.RemoteShipController = Instantiate(RemotePlayerPrefab, spawnPos, Quaternion.identity).GetComponent<RemoteShipController>();
                rm.RemotePlayer.RemoteShipController.OwnerClientID = id;
                rm.RemotePlayer.RemoteShipController.NetworkMgr = this;
                return;
            }
        }
    }
}
