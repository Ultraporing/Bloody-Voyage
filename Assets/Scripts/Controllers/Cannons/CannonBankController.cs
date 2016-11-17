using UnityEngine;
using System.Collections;
using System;

namespace Controllers.Cannons
{
    [Serializable]
    public enum CannonBankSide
    {
        Left, Right
    }

    [Serializable]
    public enum CannonBankPosition
    {
        Back, Middle, Front
    }

    [Serializable]
    public class CannonBankControllers
    {
        public CannonBankPosition Position;
        public CannonBankSide Side;
        public CannonBankController CannonBank;
    }

    public class CannonBankController : MonoBehaviour
    {
        public CannonBaseController[] CannonBaseControllers;
        public GameObject CameraRig;
        public Vector3 TargetRot;
        public Vector3 LastTargetRot;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (TargetRot != LastTargetRot)
            {
                SetTargetRotation(TargetRot);
                LastTargetRot = TargetRot;
            }
        }

        public void Deactivate()
        {
            foreach (CannonBaseController cbc in CannonBaseControllers)
            {
                cbc.Deactivate();
            }

            CameraRig.SetActive(false);
        }

        public void Activate()
        {
            foreach (CannonBaseController cbc in CannonBaseControllers)
            {
                cbc.Activate();
            }

            CameraRig.SetActive(true);
        }

        public void SetTargetRotation(Vector3 rot)
        {
            foreach (CannonBaseController cbc in CannonBaseControllers)
            {
                cbc.SetTargetRotation(rot);
                TargetRot = rot;
            }
        }

        public Vector3 GetTargetRotation()
        {
            return TargetRot;
        }
    }
}
