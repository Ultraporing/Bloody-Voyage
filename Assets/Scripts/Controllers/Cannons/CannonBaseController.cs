using UnityEngine;
using System.Collections;
using System;

namespace Controllers.Cannons
{
    public class CannonBaseController : MonoBehaviour
    {
        public CannonController CannonController = null;
        public float CannonBaseMinXRotation = -15;
        public float CannonBaseMaxXRotation = 15;
        public float CannonBaseTargetXRotation = 0;

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
            CannonController.Deactivate();
        }

        public void Activate()
        {
            CannonController.Activate();
        }

        // only uses X part
        public void SetTargetRotation(Vector3 rot)
        {
            CannonBaseTargetXRotation = Mathf.Clamp(rot.x, CannonBaseMinXRotation, CannonBaseMaxXRotation);
            Rotate();
            CannonController.SetTargetRotation(rot);
        }

        private void Rotate()
        {
            Quaternion target = Quaternion.Euler(CannonBaseTargetXRotation, transform.localRotation.y, -90);
            transform.localRotation = Quaternion.Slerp(transform.rotation, target, 1);
        }

        public void Fire()
        {
            CannonController.Fire();
        }
    }
}
