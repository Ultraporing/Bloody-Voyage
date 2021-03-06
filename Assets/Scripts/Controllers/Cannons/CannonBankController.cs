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

    [Serializable]
    public enum CannonRotationType
    {
        Fixed, Manual
    }

    public class CannonBankController : MonoBehaviour
    {
        public CannonRotationType CannonRotationType = CannonRotationType.Fixed;
        public CannonController[] CannonControllers;
        public GameObject CameraRig;
        public Vector3 TargetRot;
        public Vector3 LastTargetRot;
        private AudioSource CannonSFX = null;

        // Use this for initialization
        void Start()
        {
            CannonSFX = transform.FindChild("CannonSFX").GetComponent<AudioSource>();
            CannonControllers = GetComponentsInChildren<CannonController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Deactivate()
        {
            foreach (CannonController cbc in CannonControllers)
            {
                cbc.Deactivate();
            }

            CameraRig.SetActive(false);
        }

        public void Activate()
        {
            foreach (CannonController cbc in CannonControllers)
            {
                cbc.Activate();
            }

            CameraRig.SetActive(true);
        }

        public void SetTargetRotation(Vector3 rot)
        {
            foreach (CannonController cbc in CannonControllers)
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
                foreach (CannonController cbc in CannonControllers)
                {
                    cbc.Fire();
                }

                CannonSFX.Play();
            }
        }

        public float GetCannonReloadStatus()
        {
            return CannonControllers[0].CannonReloadingTimeLeft;
        }

        public float GetCannonReloadTimeNeeded()
        {
            return CannonControllers[0].CannonReloadTimeSec;
        }
    }
}
