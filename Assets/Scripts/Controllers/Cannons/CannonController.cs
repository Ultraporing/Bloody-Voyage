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
        public float CannonReloadTimeSec = 10;
        public float CannonReloadingTimeLeft = 0;
        public GameObject CannonballPrefab = null;
        private GameObject SightLine = null;
        private ParticleSystem ShootParticleEffect = null;
        private bool isActive = false;
        private PlayerFire PlayerFire = null;

        // Use this for initialization
        void Start()
        {
            SightLine = transform.FindChild("SightLine").gameObject;
            PlayerFire = SightLine.GetComponent<PlayerFire>();
            ShootParticleEffect = transform.FindChild("WhiteSmoke").GetComponent<ParticleSystem>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (CannonReloadingTimeLeft > 0 && SightLine.activeSelf && isActive)
            {
                SightLine.SetActive(false);
            }
            else if (CannonReloadingTimeLeft <= 0 && !SightLine.activeSelf && isActive)
            {
                SightLine.SetActive(true);
            }

            if (CannonReloadingTimeLeft > 0)
            {
                CannonReloadingTimeLeft -= Time.deltaTime;
                if (CannonReloadingTimeLeft < 0)
                {
                    CannonReloadingTimeLeft = 0;
                }
            }
        }

        public void Deactivate()
        {
            isActive = false;
            SightLine.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
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

        public void Fire()
        {
            if (CannonReloadingTimeLeft <= 0)
            {
                SightLine.SetActive(false);
                ShootParticleEffect.Play();
                ReloadCannon();
                GameObject go = (GameObject)Instantiate(CannonballPrefab, SightLine.transform.position, ShootParticleEffect.transform.rotation);
                go.GetComponent<CannonballController>().SendForce(PlayerFire.fireStrength); 
            }
        }

        private void ReloadCannon()
        {
            CannonReloadingTimeLeft = CannonReloadTimeSec;
        }
    }
}
