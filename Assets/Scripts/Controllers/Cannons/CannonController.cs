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
        public bool CannonReloading = false;
        public GameObject CannonballPrefab = null;
        private GameObject SightLine = null;
        private ParticleSystem ShootParticleEffect = null;
        private bool isActive = false;
        

        // Use this for initialization
        void Start()
        {
            SightLine = transform.FindChild("SightLine").gameObject;
            ShootParticleEffect = transform.FindChild("WhiteSmoke").GetComponent<ParticleSystem>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (CannonReloading && SightLine.activeSelf && isActive)
            {
                SightLine.SetActive(false);
            }
            else if (!CannonReloading && !SightLine.activeSelf && isActive)
            {
                SightLine.SetActive(true);
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
            if (!CannonReloading)
            {
                Vector3 spawnPos = SightLine.transform.position;
                SightLine.SetActive(false);
                ShootParticleEffect.Play();
                StartCoroutine(ReloadCannon());
                Instantiate(CannonballPrefab, SightLine.transform.position, ShootParticleEffect.transform.rotation);  
            }
        }

        private IEnumerator ReloadCannon()
        {
            CannonReloading = true;
            yield return new WaitForSeconds(CannonReloadTimeSec); // waits 3 seconds
            CannonReloading = false; // will make the update method pick up 
        }
    }
}
