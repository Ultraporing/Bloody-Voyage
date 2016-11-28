using Controllers.Sails;
using Controllers.Cannons;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

namespace Controllers.Vehicles.Ship
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
        public GameObject shipCamera = null;
        public float rotSpeed = 2;

        private int CurrentSailStage = 0;
        private float LastV = 0;
        private CannonBankControllers CurrentActiveCannonBank = null;
        private int ActiveCannonbankHash = 0;
        public ShipMode currentShipMode = ShipMode.Sailing;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<ShipController>();
            foreach (SailController sc in SailControllers)
            {
                sc.LowerSails();
                sc.LowerSails();
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            CurrentSailStage = SailControllers[0].GetCurrentSailStage();

            if (currentShipMode == ShipMode.Sailing)
            {
                HandleSailingInput();

                if (CrossPlatformInputManager.GetButtonUp("FireLeftCannonbanks"))
                {
                    foreach (CannonBankControllers cbc in CannonBankControllers)
                    {
                        if (cbc.Side == CannonBankSide.Left)
                        {
                            cbc.CannonBank.Fire();
                        }
                    }
                }

                if (CrossPlatformInputManager.GetButtonUp("FireRightCannonbanks"))
                {
                    foreach (CannonBankControllers cbc in CannonBankControllers)
                    {
                        if (cbc.Side == CannonBankSide.Right)
                        {
                            cbc.CannonBank.Fire();
                        }
                    }
                }
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

        private void FixedUpdate()
        {
            float h = 0;

            if (currentShipMode == ShipMode.Sailing)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
            }
            else if (currentShipMode == ShipMode.Firing)
            {
                HandleCannonRotation();
            }
            
            m_Car.Move(h, LastV, LastV, 0f);
           // Debug.Log("h: " + h + ", v: " + v);

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
            else if (Input.GetButtonUp("Fire"))
            {
                CurrentActiveCannonBank.CannonBank.Fire();
            }
        }

        private void HandleCannonRotation()
        {
            if (CurrentActiveCannonBank.CannonBank.CannonRotationType == CannonRotationType.Fixed)
                return;

            Vector3 targetRot = CurrentActiveCannonBank.CannonBank.GetTargetRotation();

            if (Input.GetAxis("Mouse X") < 0)
            {
                targetRot.x += -rotSpeed*Time.deltaTime;
            }
            else if (Input.GetAxis("Mouse X") > 0)
            {
                targetRot.x += rotSpeed * Time.deltaTime;
            }

            if (Input.GetAxis("Mouse Y") < 0)
            {
                targetRot.z += -rotSpeed * Time.deltaTime;
            }
            else if (Input.GetAxis("Mouse Y") > 0)
            {
                targetRot.z += rotSpeed * Time.deltaTime;
            }

            CurrentActiveCannonBank.CannonBank.SetTargetRotation(targetRot);
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
            if (ActiveCannonbankHash != 0)
            {
                CurrentActiveCannonBank.CannonBank.Deactivate();
                ActiveCannonbankHash = 0;
            }

            if (cbc != null)
            {
                CurrentActiveCannonBank = cbc;
                CurrentActiveCannonBank.CannonBank.Activate();
                ActiveCannonbankHash = cbc.GetHashCode();
            }
        }

        public float GetCannonReloadStatus()
        {
            return CurrentActiveCannonBank.CannonBank.GetCannonReloadStatus();
        }

        public float GetCannonReloadTimeNeeded()
        {
            return CurrentActiveCannonBank.CannonBank.GetCannonReloadTimeNeeded();
        }

        public CannonBankControllers GetActiveCannonBank()
        {
            return ActiveCannonbankHash != 0 ? CurrentActiveCannonBank : null;
        }
    }
}
