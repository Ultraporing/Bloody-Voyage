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
        private AudioSource CannonSFX = null;

        // Use this for initialization
        void Start()
        {
            CannonSFX = transform.FindChild("CannonSFX").GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

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

        public void Fire()
        {
            if (GetCannonReloadStatus() <= 0)
            {
                foreach (CannonBaseController cbc in CannonBaseControllers)
                {
                    cbc.Fire();
                }

                CannonSFX.Play();
            }
        }

        public float GetCannonReloadStatus()
        {
            return CannonBaseControllers[0].CannonController.CannonReloadingTimeLeft;
        }

        public float GetCannonReloadTimeNeeded()
        {
            return CannonBaseControllers[0].CannonController.CannonReloadTimeSec;
        }
    }
}
