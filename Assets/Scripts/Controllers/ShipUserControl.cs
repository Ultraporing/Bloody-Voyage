using Controllers.Sails;
using Controllers.Cannons;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

namespace UnityStandardAssets.Vehicles.Car
{
    public enum ShipMode
    {
        Sailing, Firing
    }

    [RequireComponent(typeof (ShipController))]
    public class ShipUserControl : MonoBehaviour
    {
        private ShipController m_Car; // the car controller we want to use
        public SailController[] SailControllers;
        public CannonBankControllers[] CannonBankControllers;
        public int CurrentSailStage = 0;
        public float LastV = 0;
        public CannonBankControllers CurrentActiveCannonBank = null;
        public ShipMode currentShipMode = ShipMode.Sailing;
        public GameObject shipCamera = null; 

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<ShipController>();
            foreach (SailController sc in SailControllers)
            {
                sc.LowerSails();
                sc.LowerSails();
            }
        }

        void Update()
        {
            CurrentSailStage = SailControllers[0].GetCurrentSailStage();

            if (currentShipMode == ShipMode.Sailing)
            {
                HandleSailingInput();
            }
            else if (currentShipMode == ShipMode.Firing)
            {
                HandleFiringInput();
            }

            if (Input.GetButtonUp("SwitchMode"))
            {
                SwitchModes();
            }
        }

        private void HandleSailingInput()
        {
            // pass the input to the car!

            bool w = CrossPlatformInputManager.GetButtonUp("HoistSails");
            bool s = CrossPlatformInputManager.GetButtonUp("LowerSails");
            float v = 0;


            if (s)
            {
                if (CurrentSailStage == 0)
                {
                    foreach (SailController sc in SailControllers)
                    {
                        sc.LowerSails();
                    }
                    v = 0.5f;
                    LastV = v;
                }
                else if (CurrentSailStage == 1)
                {
                    foreach (SailController sc in SailControllers)
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
                    foreach (SailController sc in SailControllers)
                    {
                        sc.HoistSails();
                    }
                    v = 1.0f;
                    LastV = v;
                }
            }
        }

        private void HandleFiringInput()
        {
            int maxEnumPos = (int)Enum.GetValues(typeof(CannonBankPosition)).Cast<CannonBankPosition>().Max();
            int minEnumPos = (int)Enum.GetValues(typeof(CannonBankPosition)).Cast<CannonBankPosition>().Min();

            // next right cannonbank
            if (Input.GetButtonUp("CannonbankRight"))
            {
                int nextCBPos = ((int)CurrentActiveCannonBank.Position) + (CurrentActiveCannonBank.Side == CannonBankSide.Left ? 1 : -1);
                
                if (nextCBPos <= maxEnumPos && nextCBPos >= minEnumPos)
                {
                    ActivateCannonBank(FindCannonBankController(CurrentActiveCannonBank.Side, (CannonBankPosition)nextCBPos));
                }
            }
            else if (Input.GetButtonUp("CannonbankLeft"))
            {
                int nextCBPos = ((int)CurrentActiveCannonBank.Position) + (CurrentActiveCannonBank.Side == CannonBankSide.Left ? -1 : 1);

                if (nextCBPos <= maxEnumPos && nextCBPos >= minEnumPos)
                {
                    ActivateCannonBank(FindCannonBankController(CurrentActiveCannonBank.Side, (CannonBankPosition)nextCBPos));
                }
            }
            else if (Input.GetButtonUp("SwitchSides"))
            {
                CannonBankSide nextSide = CannonBankSide.Right;

                if (CurrentActiveCannonBank.Side == CannonBankSide.Right)
                {
                    nextSide = CannonBankSide.Left;
                }
                else
                {
                    nextSide = CannonBankSide.Right;
                }
                
                ActivateCannonBank(FindCannonBankController(nextSide, CurrentActiveCannonBank.Position));
                
            }

            if (Input.GetButtonUp("SwitchMode"))
            {
                SwitchModes();
            }
        }

        private void FixedUpdate()
        {
            float h = 0;

            if (currentShipMode == ShipMode.Sailing)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
            }
            
            m_Car.Move(h, LastV, LastV, 0f);
           // Debug.Log("h: " + h + ", v: " + v);

        }

        private void SwitchModes()
        {
            if (currentShipMode == ShipMode.Sailing)
            {
                currentShipMode = ShipMode.Firing;
                shipCamera.SetActive(false);
                ActivateCannonBank(FindCannonBankController(CannonBankSide.Right, CannonBankPosition.Middle));
            }
            else
            {
                currentShipMode = ShipMode.Sailing;
                ActivateCannonBank(null);
                shipCamera.SetActive(true);
            }
        }

        public CannonBankControllers FindCannonBankController(CannonBankSide side, CannonBankPosition pos)
        {
            foreach (CannonBankControllers cbc in CannonBankControllers)
            {
                if (pos == cbc.Position && side == cbc.Side)
                {
                    return cbc;
                }
            }

            return null;
        }

        // Provide cannonbank to activate. To deactivate cannonbank just pass null.
        public void ActivateCannonBank(CannonBankControllers cbc)
        {
            if (CurrentActiveCannonBank.CannonBank != null)
            {
                CurrentActiveCannonBank.CannonBank.Deactivate();
                CurrentActiveCannonBank = null;
            }

            if (cbc != null)
            {
                CurrentActiveCannonBank = cbc;
                CurrentActiveCannonBank.CannonBank.Activate();
            }
        }
    }
}
