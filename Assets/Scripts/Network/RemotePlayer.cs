using FastSockets.Networking;
using Network.Controllers.Vehicles.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    [Serializable]
    public class RemotePlayer
    {
        public RemoteShipController RemoteShipController = null;

        public RemotePlayer()
        {
            
        }
        
    }
}

