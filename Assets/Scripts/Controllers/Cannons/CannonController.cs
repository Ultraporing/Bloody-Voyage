using UnityEngine;
using System.Collections;
using System;

namespace Controllers.Cannons
{
    public class CannonController : MonoBehaviour
    {
        public float CannonMinZRotation = -90;
        public float CannonMaxZRotation = -45;
        public float CannonTargetZRotation = -90;
        public GameObject SightLine = null;

        // Use this for initialization
        void Start()
        {
            SightLine = transform.FindChild("SightLine").gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Deactivate()
        {
            SightLine.SetActive(false);
        }

        public void Activate()
        {
            SightLine.SetActive(true);
        }

        // only uses Z part
        public void SetTargetRotation(Vector3 rot)
        {
            CannonTargetZRotation = Mathf.Clamp(rot.z, CannonMinZRotation, CannonMaxZRotation);
            Rotate();
        }

        private void Rotate()
        {
            Quaternion target = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, CannonTargetZRotation);
            transform.localRotation = Quaternion.Slerp(transform.rotation, target, 1);
        }
    }
}
