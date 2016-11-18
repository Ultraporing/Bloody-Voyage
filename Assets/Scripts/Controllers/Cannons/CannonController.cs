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

        private GameObject SightLine = null;
        private ParticleSystem ShootParticleEffect = null;
        private AudioSource CannonSFX = null;

        // Use this for initialization
        void Start()
        {
            SightLine = transform.FindChild("SightLine").gameObject;
            ShootParticleEffect = transform.FindChild("WhiteSmoke").GetComponent<ParticleSystem>();
            CannonSFX = transform.FindChild("CannonSFX").GetComponent<AudioSource>();
            CannonSFX.Stop();
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

        public void Fire()
        {
            if (!CannonReloading)
            {
                ShootParticleEffect.Play();
                CannonSFX.Play();
                StartCoroutine(ReloadCannon());
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
