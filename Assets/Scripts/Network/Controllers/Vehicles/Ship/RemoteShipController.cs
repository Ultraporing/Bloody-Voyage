using Controllers.Cannons;
using Controllers.Sails;
using System;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

namespace Network.Controllers.Vehicles.Ship
{
    public class RemoteShipController : MonoBehaviour
    {
        public int OwnerClientID = -1;
        public SailController[] SailControllers;
        public CannonBankControllers[] CannonBankControllers;
        private CannonBankControllers CurrentActiveCannonBank = null;
        private int ActiveCannonbankHash = 0;
        private int CurrentSailStage = 0;
        public int CurrentSpeed = 0;
        public NetworkManager NetworkMgr = null;

        // Use this for initialization
        private void Start()
        {

        }

        private void Update()
        {
            CurrentSailStage = SailControllers[0].GetCurrentSailStage();

            if (NetworkMgr.FindRemotePlayerWithID(OwnerClientID) == null)
            {
                Destroy(gameObject);
            }
        }

        public void Network_Fire(CannonBankSide side, CannonBankPosition pos)
        {
            foreach (CannonBankControllers cbc in CannonBankControllers)
            {
                if (cbc.Side == side && cbc.Position == pos)
                {
                    cbc.CannonBank.Fire();
                    return;
                }
            }
        }

        public void Network_FireAllOnSide(CannonBankSide side)
        {
            foreach (CannonBankControllers cbc in CannonBankControllers)
            {
                if (cbc.Side == side)
                {
                    cbc.CannonBank.Fire();
                }
            }
        }

        /// <summary>
        /// Network. set target SailStage. 0 = full sail, 1 = half sail, 2 = no sail
        /// </summary>
        /// <param name="targetStage">The target SailStage.</param>
        public void Network_SetTargetStage(int targetStage)
        {
            foreach (SailController sc in SailControllers)
            {
                sc.Network_SetTargetStage(targetStage);
            }
        }

        public void Network_SetTransform(Vector3 worldPos, Vector3 rotEuler, Vector3 localScale)
        {
            transform.position = worldPos;
            transform.rotation = Quaternion.Euler(rotEuler);
            transform.localScale = localScale;
        }

        public void Network_SetCurrentSpeed(int currentSpeed)
        {
            CurrentSpeed = currentSpeed;
        }
    }
}
