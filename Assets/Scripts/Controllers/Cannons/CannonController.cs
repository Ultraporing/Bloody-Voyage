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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Deactivate()
        {
            
        }

        public void Activate()
        {
            
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
