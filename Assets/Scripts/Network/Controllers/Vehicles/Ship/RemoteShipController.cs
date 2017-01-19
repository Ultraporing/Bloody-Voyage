using Controllers.Cannons;
using Controllers.Sails;
using System;
using UnityEngine;
using UnityEngine.UI;
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
        public Rigidbody Rigidbody = null;

        public Vector3 TargetPos, LastTargetPos;
        public Quaternion TargetRot, LastTargetRot;
        public int TotalPing = 0;
        public float LerpElapsed = 0;
        public bool QuickFinishLerp = false;
        public bool LerpFinished = true;
        public Text Ping;
        

        public CameraFacingBillboard cfb = null;
        

        // Use this for initialization
        private void Start()
        {
            cfb = transform.Find("Canvas").GetComponent<CameraFacingBillboard>();
            cfb.m_Camera = Camera.main;

            Ping = transform.Find("Canvas").Find("Panel").Find("Text").GetComponent<Text>();
        }

        private void Update()
        {
            CurrentSailStage = SailControllers[0].GetCurrentSailStage();

            if (NetworkMgr.FindRemotePlayerWithID(OwnerClientID) == null)
            {
                Destroy(gameObject);
            }

            if (LastTargetPos != TargetPos)
            {
                UpdatePlayerPosRot();
            }

            

            Ping.text = TotalPing + "ms";
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

        public void Network_SetTransform(Vector3 worldPos, Vector3 rotEuler, Vector3 localScale, int ping)
        {
            if (!LerpFinished)
            {
                QuickFinishLerp = true;
            }
            
            LastTargetPos = TargetPos;
            LastTargetRot = TargetRot;

            TargetPos = worldPos;
            TargetRot = Quaternion.Euler(rotEuler);
            transform.localScale = localScale;
            TotalPing = ping;

            LerpFinished = false;
        }

        public void UpdatePlayerPosRot()
        {          
            if (QuickFinishLerp)
            {
                LerpElapsed = 0;
                transform.position = Vector3.Slerp(transform.position, LastTargetPos, 1.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, LastTargetRot, 1.0f);

                QuickFinishLerp = false;
            }

            if (!LerpFinished)
            {
                LerpElapsed += 1/TotalPing;

                transform.position = Vector3.Slerp(transform.position, TargetPos, LerpElapsed);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRot, LerpElapsed);

                if (LerpElapsed >= 1.0f)
                {
                    LerpElapsed = 0;
                    LerpFinished = true;
                }
            }
        }

        public void Network_SetCurrentSpeed(int currentSpeed)
        {
            CurrentSpeed = currentSpeed;
        }
    }
}
