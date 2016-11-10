using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (ShipController))]
    public class ShipUserControl : MonoBehaviour
    {
        private ShipController m_Car; // the car controller we want to use
        public Controllers.Sails.SailController[] SailControllers;
        public int CurrentSailStage = 0;
        public float LastV = 0;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<ShipController>();
            foreach (Controllers.Sails.SailController sc in SailControllers)
            {
                sc.LowerSails();
                sc.LowerSails();
            }
        }

        void Update()
        {
            CurrentSailStage = SailControllers[0].GetCurrentSailStage();

            // pass the input to the car!
            
            bool w = CrossPlatformInputManager.GetButtonUp("HoistSails");
            bool s = CrossPlatformInputManager.GetButtonUp("LowerSails");
            float v = 0;


            if (s)
            {
                if (CurrentSailStage == 0)
                {
                    foreach (Controllers.Sails.SailController sc in SailControllers)
                    {
                        sc.LowerSails();
                    }
                    v = 0.5f;
                    LastV = v;
                }
                else if (CurrentSailStage == 1)
                {
                    foreach (Controllers.Sails.SailController sc in SailControllers)
                    {
                        sc.LowerSails();
                    }
                    v = 0;
                    LastV = v;
                }
            }
            else if (w)
            {
                if (CurrentSailStage == 2)
                {
                    foreach (Controllers.Sails.SailController sc in SailControllers)
                    {
                        sc.HoistSails();
                    }
                    v = 0.5f;
                    LastV = v;
                }
                else if (CurrentSailStage == 1)
                {
                    foreach (Controllers.Sails.SailController sc in SailControllers)
                    {
                        sc.HoistSails();
                    }
                    v = 1.0f;
                    LastV = v;
                }
            }
        }

        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");


            m_Car.Move(h, LastV, LastV, 0f);
           // Debug.Log("h: " + h + ", v: " + v);

        }
    }
}
